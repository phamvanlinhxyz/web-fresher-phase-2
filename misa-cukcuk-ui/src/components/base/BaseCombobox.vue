<template>
  <div class="combobox" ref="comboboxRef">
    <input
      class="cbb-input"
      type="text"
      @input="handleInput"
      ref="comboboxInput"
      :disabled="disabled"
    />
    <base-icon iconName="grey-drop-arrow" @click="handleArrowClick" />
    <div class="add-button" v-if="addIcon" @click="handleAddItem">
      <base-icon iconName="add" />
    </div>
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
  props: ["listItem", "addIcon", "tableName", "disabled", "value"],
  emits: ["change"],
  data() {
    return {
      isShowDropdown: false,
      dropdownTop: 0,
      dropdownLeft: 0,
      dropdownWidth: 0,
      filterItems: [],
      selectedItem: null,
    };
  },
  watch: {
    selectedItem(newItem, oldItem) {
      this.$emit("change", newItem);
    },
  },
  methods: {
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
    },
    /**
     * Xử lý autocomplete khi nhập
     * @param {*} event
     * Author: linhpv (07/08/2022)
     */
    handleInput(event) {
      // Lọc giá trị
      let input = event.target.value;
      this.filterItems = this.listItem.filter((item) =>
        item[`${this.tableName}Name`]
          .toLowerCase()
          .includes(input.toLowerCase())
      );
      // Setup vị trí
      this.setupPosition();
      this.isShowDropdown = true;
      // Auto select nếu đúng tên
      let findItem = this.listItem.find((item) => {
        return item[`${this.tableName}Name`] === input;
      });
      if (findItem) {
        this.selectedItem = findItem;
        this.isShowDropdown = false;
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
  created() {},
  mounted() {},
  updated() {
    this.filterItems = this.listItem;
    if (this.value) {
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