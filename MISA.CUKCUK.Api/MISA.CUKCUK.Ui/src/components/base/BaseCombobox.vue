<template>
  <div :class="comboboxClass" ref="comboboxRef">
    <input
      class="cbb-input"
      type="text"
      @input="handleInput"
      @keydown="handleKeyDown"
      @keyup="handleKeyUp"
      ref="comboboxInput"
      :disabled="disabled"
      v-on:focusin="() => (this.isFocus = true)"
      v-on:blur="handleBlurInput"
    />
    <base-icon iconName="grey-drop-arrow" @click="handleArrowClick" />
    <div class="add-button" v-if="addIcon" @click="handleAddItem">
      <base-icon iconName="add" />
    </div>
  </div>
  <div class="input-error-icon" v-if="errorMessage">
    <base-icon iconName="danger" :title="errorMessage" />
  </div>
  <!-- <div class="cbb-bg" @click="toggleDropdown" v-show="isShowDropdown"></div> -->
  <div
    v-if="dropdownType != 1 && isShowDropdown"
    class="cbb-option"
    :style="{
      top: `${dropdownTop}px`,
      left: `${dropdownLeft}px`,
      width: `${dropdownWidth}px`,
    }"
    ref="comboboxOption"
    v-click-outside="toggleDropdown"
  >
    <ul class="cbb-list-item">
      <li
        class="cbb-item"
        v-for="item of filterItems"
        :key="item[`${tableName}ID`]"
        @click="handleSelect(item)"
        :class="{
          'item-selected':
            selectedItem &&
            item[`${tableName}ID`] == selectedItem[`${tableName}ID`],
        }"
      >
        {{ item[`${tableName}Name`] }}
      </li>
    </ul>
  </div>
  <div
    v-if="dropdownType == 1 && isShowDropdown"
    class="cbb-option"
    v-show="isShowDropdown"
    :style="{
      top: `${dropdownTop}px`,
      left: `${dropdownLeft}px`,
    }"
    ref="comboboxOption"
    v-click-outside="toggleDropdown"
  >
    <ul class="cbb-list-item-table">
      <li class="cbb-item-table table-title">
        <div class="item-code">Mã</div>
        <div class="item-name">Tên</div>
      </li>
      <li
        class="cbb-item-table"
        v-for="item of filterItems"
        :key="item[`${tableName}ID`]"
        @click="handleSelect(item)"
        :class="{
          'item-selected':
            selectedItem &&
            item[`${tableName}ID`] == selectedItem[`${tableName}ID`],
        }"
      >
        <div class="item-code">
          {{ item[`${tableName}Code`] }}
        </div>
        <div class="item-name">
          {{ item[`${tableName}Name`] }}
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: [
    "listItem",
    "addIcon",
    "tableName",
    "disabled",
    "value",
    "focus",
    "errorMessage",
    "hideBorder",
    "dropdownType",
    "columnShow",
  ],
  emits: ["change"],
  data() {
    return {
      comboboxClass: "combobox",
      isShowDropdown: false,
      dropdownTop: 0,
      dropdownLeft: 0,
      dropdownWidth: 0,
      filterItems: [],
      selectedItem: null,
      selectedIndex: 0,
      show: "",
      isFocus: false,
    };
  },
  watch: {
    focus(newVal) {
      if (newVal) {
        this.$refs.comboboxInput.focus();
      }
    },
    errorMessage(newVal) {
      if (newVal == null) {
        this.comboboxClass = this.comboboxClass.filter(
          (item) => item !== "combobox-error"
        );
      } else {
        this.comboboxClass = [...this.comboboxClass, "combobox-error"];
      }
    },
    value(newVal) {
      this.selectedItem = this.listItem.find((item) => {
        return item[`${this.tableName}ID`] == newVal;
      });
      if (this.selectedItem) {
        this.$refs.comboboxInput.value = this.selectedItem[this.show];
        this.$emit("change", this.selectedItem, this.tableName);
      } else {
        this.$refs.comboboxInput.value = null;
      }
    },
    isFocus(newVal) {
      if (newVal) {
        this.comboboxClass = [...this.comboboxClass, "combobox-focus"];
      } else {
        this.comboboxClass = this.comboboxClass.filter(
          (item) => item !== "combobox-focus"
        );
      }
    },
  },
  methods: {
    /**
     * Xử lý các sự kiện keyup như lên xuống chọn bản ghi
     * @param {*} event
     * Author: linhpv (14/08/2022)
     */
    handleKeyUp(event) {
      var key = event.key;
      // Sự kiện ấn nút mũi tên xuống
      if (key === "ArrowDown") {
        // Check filteritem, nếu chưa có giá trị thì gán cho tất cả item
        if (this.filterItems.length === 0) {
          this.filterItems = this.listItem;
        }
        // Check dropdown nếu chưa mở thì mở
        if (!this.isShowDropdown) {
          this.setupPosition();
          this.isShowDropdown = true;
          this.selectedIndex = 0;
        } else if (this.selectedIndex < this.filterItems.length - 1) {
          this.selectedIndex++;
        }
        // Lấy vị trí của item được lựa chọn
        this.selectedItem = this.filterItems[this.selectedIndex];
      }
      if (key === "ArrowUp") {
        // check dropdown có đang mở hay không và vị trí có khác vị trí đầu không
        if (this.isShowDropdown && this.selectedIndex > 0) {
          this.selectedIndex--;
        }
        // Lấy vị trí của item được lựa chọn
        this.selectedItem = this.filterItems[this.selectedIndex];
      }
    },
    /**
     * Xử lý các sự kiện keydown trên combobox, VD: Tab, Enter
     * @param {*} event
     * Author: linhpv (14/08/2022)
     */
    handleKeyDown(event) {
      var key = event.key;
      // Các sự kiện đóng dropdown
      if (key === "Tab" || key === "Enter") {
        this.isShowDropdown = false;
        if (this.filterItems.length > 0) {
          this.selectedItem = this.filterItems[this.selectedIndex];
          event.target.value = this.selectedItem[this.show];
          this.$emit("change", this.selectedItem, this.tableName);
        }
      }
    },
    /**
     * Khi người dùng blur ra khỏi input
     * Author: linhpv(14/08/2022)
     */
    handleBlurInput() {
      // Bỏ border xanh
      this.isFocus = false;
      // Nếu có item đang được chọn thì set value nếu không thì value = null
      if (this.selectedItem) {
        this.$refs.comboboxInput.value = this.selectedItem[this.show];
      } else {
        this.$refs.comboboxInput.value = null;
      }
    },
    /**
     * Xự kiện người dùng ấn thêm item
     * Author: linhpv (11/08/2022)
     */
    handleAddItem() {
      this.$store.dispatch(`toggle${this.tableName}Popup`);
    },
    /**
     * Xự kiện click chọn item
     * @param {object} item item đã chọn
     * Author: linhpv (07/08/2022)
     */
    handleSelect(item) {
      this.$refs.comboboxInput.value = item[this.show];
      this.selectedItem = item;
      this.$emit("change", this.selectedItem, this.tableName);
      this.toggleDropdown();
    },
    /**
     * Xử lý xự kiện người dùng ấn button mũi tên xuống
     * Author: linhpv (07/08/2022)
     */
    handleArrowClick() {
      this.filterItems = this.listItem;
      this.toggleDropdown();
      this.$refs.comboboxInput.focus();
    },
    /**
     * Xử lý autocomplete khi nhập
     * @param {*} event
     * Author: linhpv (07/08/2022)
     */
    handleInput(event) {
      // Lọc giá trị
      let input = event.target.value;
      if (input !== "") {
        this.filterItems = this.listItem.filter((item) =>
          item[`${this.tableName}Name`]
            .toLowerCase()
            .includes(input.toLowerCase())
        );
      } else {
        this.filterItems = [];
      }
      // Nếu lọc có giá trị thì select cái đầu tiên
      if (this.filterItems.length > 0) {
        this.selectedIndex = 0;
        this.selectedItem = this.filterItems[this.selectedIndex];
        // Setup vị trí
        this.setupPosition();
        this.isShowDropdown = true;
      } else {
        this.selectedItem = null;
      }
      if (!this.selectedItem) {
        this.$emit("change", this.selectedItem, this.tableName);
      }
    },
    /**
     * Đóng mở dropdown
     * Author: linhpv (07/08/2022)
     */
    toggleDropdown() {
      if (!this.isShowDropdown) {
        this.setupPosition();
      }
      this.isShowDropdown = !this.isShowDropdown;
    },
    /**
     * Đăt vị trí cho dropdown
     * Author: linhpv (10/08/2022)
     */
    setupPosition() {
      let rect = this.$refs.comboboxRef.getBoundingClientRect();
      this.dropdownLeft = rect.left;
      this.dropdownWidth = rect.width;
      let height =
        24 * this.listItem.length < 300 ? 24 * this.listItem.length : 300;
      if (rect.bottom + height < window.innerHeight) {
        this.dropdownTop = rect.top + rect.height;
      } else {
        this.dropdownTop = rect.top - height;
      }
    },
  },
  created() {
    this.comboboxClass = ["combobox", this.hideBorder ? "hide-border" : null];
    if (this.columnShow) {
      this.show = this.columnShow;
    } else {
      this.show = `${this.tableName}Name`;
    }
  },
  mounted() {
    if (this.value && !this.selectedItem && this.listItem.length !== 0) {
      this.selectedItem = this.listItem.find((item) => {
        return item[`${this.tableName}ID`] == this.value;
      });
      this.$refs.comboboxInput.value = this.selectedItem[this.show];
    }
    if (this.focus) {
      this.$refs.comboboxInput.focus();
    }
  },
  updated() {
    if (!this.filterItems) {
      this.filterItems = this.listItem;
    }
    // if (this.value && !this.selectedItem) {
    //   this.selectedItem = this.listItem.find((item) => {
    //     return item[`${this.tableName}ID`] == this.value;
    //   });
    //   this.$refs.comboboxInput.value = this.selectedItem[this.show];
    //   this.$emit("change", this.selectedItem);
    // }
  },
};
</script>

<style scoped>
@import url(@/css/base/basecombobox.css);
</style>