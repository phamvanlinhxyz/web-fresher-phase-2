<template>
  <div class="combobox" ref="comboboxRef">
    <input
      class="cbb-input"
      type="text"
      @input="handleInput"
      ref="comboboxInput"
    />
    <base-icon iconName="grey-drop-arrow" @click="handleArrowClick" />
    <div class="add-button" v-if="addIcon">
      <base-icon iconName="add" />
    </div>
  </div>
  <div class="cbb-bg" @click="toggleDropdown" v-if="isShowDropdown"></div>
  <div
    class="cbb-option"
    v-if="isShowDropdown"
    :style="{
      top: `${dropdownTop}px`,
      left: `${dropdownLeft}px`,
      width: `${dropdownWidth}px`,
    }"
  >
    <ul class="cbb-list-item">
      <li
        class="cbb-item"
        v-for="item of filterItems"
        :key="item[`${tableName}ID`]"
        @click="handleSelect(item)"
        :class="{
          'item-selected':
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
  props: ["listItem", "addIcon", "tableName"],
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
  methods: {
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
      let rect = this.$refs.comboboxRef.getBoundingClientRect();
      this.dropdownTop = rect.top + rect.height;
      this.dropdownLeft = rect.left;
      this.dropdownWidth = rect.width;
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
        let rect = this.$refs.comboboxRef.getBoundingClientRect();
        this.dropdownTop = rect.top + rect.height;
        this.dropdownLeft = rect.left;
        this.dropdownWidth = rect.width;
      }
      this.isShowDropdown = !this.isShowDropdown;
    },
  },
  created() {
    this.filterItems = this.listItem;
    if (this.filterItems.length > 0) {
      this.selectedItem = this.filterItems[0];
    } else {
      this.selectedItem[`${this.tableName}ID`] = null;
    }
  },
};
</script>

<style scoped>
@import url(@/css/base/basecombobox.css);
</style>