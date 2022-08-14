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
      v-on:focus="this.$refs.comboboxRef.classList.add('combobox-focus')"
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
  <div class="cbb-bg" @click="toggleDropdown" v-show="isShowDropdown"></div>
  <div
    class="cbb-option"
    v-show="isShowDropdown"
    :style="{
      top: `${dropdownTop}px`,
      left: `${dropdownLeft}px`,
      width: `${dropdownWidth}px`,
    }"
    ref="comboboxOption"
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
    };
  },
  watch: {
    selectedItem(newItem) {
      this.$emit("change", newItem);
    },
    focus(newVal) {
      if (newVal) {
        this.$refs.comboboxInput.focus();
      }
    },
    errorMessage(newVal) {
      if (newVal == null) {
        this.comboboxClass = ["combobox"];
      } else {
        this.comboboxClass = ["combobox", "combobox-error"];
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
          event.target.value = this.selectedItem[`${this.tableName}Name`];
        }
      }
    },
    /**
     * Khi người dùng blur ra khỏi input
     * Author: linhpv(14/08/2022)
     */
    handleBlurInput() {
      // Bỏ border xanh
      this.$refs.comboboxRef.classList.remove("combobox-focus");
      // Nếu có item đang được chọn thì set value nếu không thì value = null
      if (this.selectedItem) {
        this.$refs.comboboxInput.value =
          this.selectedItem[`${this.tableName}Name`];
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
     * @param {*} item
     * Author: linhpv (07/08/2022)
     */
    handleSelect(item) {
      this.$refs.comboboxInput.value = item[`${this.tableName}Name`];
      this.selectedItem = item;
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
      if (rect.bottom + 24 * this.listItem.length < window.innerHeight) {
        this.dropdownTop = rect.top + rect.height;
      } else {
        this.dropdownTop = rect.top - 24 * this.listItem.length;
      }
    },
  },
  mounted() {
    if (this.value && !this.selectedItem && this.listItem.length !== 0) {
      this.selectedItem = this.listItem.find((item) => {
        return item[`${this.tableName}ID`] == this.value;
      });
      this.$refs.comboboxInput.value =
        this.selectedItem[`${this.tableName}Name`];
    }
    if (this.focus) {
      this.$refs.comboboxInput.focus();
    }
  },
  updated() {
    if (!this.filterItems) {
      this.filterItems = this.listItem;
    }
    if (this.value && !this.selectedItem) {
      this.selectedItem = this.listItem.find((item) => {
        return item[`${this.tableName}ID`] == this.value;
      });
      this.$refs.comboboxInput.value =
        this.selectedItem[`${this.tableName}Name`];
    }
  },
};
</script>

<style scoped>
@import url(@/css/base/basecombobox.css);
</style>