<template>
  <table class="table">
    <thead class="table-header">
      <tr>
        <td style="min-width: 159px">
          <div class="table-column">
            {{ resources[`${langCode}_Table`].dishType }}
          </div>
          <div class="table-filter">
            <input
              class="input"
              :value="resources[`${langCode}_Table`].dishType"
              disabled
            />
          </div>
        </td>
        <td style="min-width: 179px">
          <div class="table-column" @click="handleSortData('DishCode')">
            <div
              :class="[
                sortObject.sortBy === 'DishCode'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].dishCode }}
            </div>
          </div>
          <div class="table-filter">
            <div
              class="filter-type"
              @click="openDropdown(enums.inputType.Text, 'DishCode', $event)"
            >
              {{ filterIcon[filterTypes.DishCode] }}
            </div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('DishCode', $event.target.value)
              "
            />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column" @click="handleSortData('DishName')">
            <div
              :class="[
                sortObject.sortBy === 'DishName'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].dishName }}
            </div>
          </div>
          <div class="table-filter">
            <div
              class="filter-type"
              @click="openDropdown(enums.inputType.Text, 'DishName', $event)"
            >
              {{ filterIcon[filterTypes.DishName] }}
            </div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('DishName', $event.target.value)
              "
            />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column" @click="handleSortData('MenuGroupName')">
            <div
              :class="[
                sortObject.sortBy === 'MenuGroupName'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].menuGroup }}
            </div>
          </div>
          <div class="table-filter">
            <div
              class="filter-type"
              @click="
                openDropdown(enums.inputType.Text, 'MenuGroupName', $event)
              "
            >
              {{ filterIcon[filterTypes.MenuGroupName] }}
            </div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('MenuGroupName', $event.target.value)
              "
            />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column" @click="handleSortData('UnitName')">
            <div
              :class="[
                sortObject.sortBy === 'UnitName'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].unit }}
            </div>
          </div>
          <div class="table-filter">
            <div
              class="filter-type"
              @click="openDropdown(enums.inputType.Text, 'UnitName', $event)"
            >
              {{ filterIcon[filterTypes.UnitName] }}
            </div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('UnitName', $event.target.value)
              "
            />
          </div>
        </td>
        <td style="min-width: 119px">
          <div class="table-column" @click="handleSortData('Price')">
            <div
              :class="[
                sortObject.sortBy === 'Price'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].price }}
            </div>
          </div>
          <div class="table-filter">
            <div
              class="filter-type"
              @click="openDropdown(enums.inputType.Number, 'Price', $event)"
            >
              {{ filterIcon[filterTypes.Price] }}
            </div>
            <base-input
              type="money"
              @change="handleFilterMoney('Price', $event.target.value)"
            />
          </div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">
            {{ resources[`${langCode}_Table`].seasonalPrice }}
          </div>
          <div class="table-filter">
            <input class="input" disabled />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">
            {{ resources[`${langCode}_Table`].flexiblePrice }}
          </div>
          <div class="table-filter">
            <input class="input" disabled />
          </div>
        </td>
        <td style="min-width: 139px">
          <div
            class="table-column"
            @click="handleSortData('MaterialQuantified')"
          >
            <div
              :class="[
                sortObject.sortBy === 'MaterialQuantified'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].materialQuantified }}
            </div>
          </div>
          <div class="table-filter">
            <base-combobox
              :listItem="[
                {
                  MaterialQuantifiedID: enums.filterType.True,
                  MaterialQuantifiedName:
                    resources[`${langCode}_Table`].quantified,
                },
                {
                  MaterialQuantifiedID: enums.filterType.False,
                  MaterialQuantifiedName:
                    resources[`${langCode}_Table`].notQuantified,
                },
              ]"
              tableName="MaterialQuantified"
              @change="selectTableCombobox"
            />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column" @click="handleSortData('ShowOnMenu')">
            <div
              :class="[
                sortObject.sortBy === 'ShowOnMenu'
                  ? 'sort-colum-' + sortObject.sortType
                  : null,
                ,
              ]"
            >
              {{ resources[`${langCode}_Table`].showOnMenu }}
            </div>
          </div>
          <div class="table-filter">
            <base-combobox
              :listItem="[
                {
                  ShowOnMenuID: enums.filterType.True,
                  ShowOnMenuName: resources[`${langCode}_Table`].yes,
                },
                {
                  ShowOnMenuID: enums.filterType.False,
                  ShowOnMenuName: resources[`${langCode}_Table`].no,
                },
              ]"
              tableName="ShowOnMenu"
              @change="selectTableCombobox"
            />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column">
            {{ resources[`${langCode}_Table`].stopSale }}
          </div>
          <div class="table-filter">
            <input
              class="input"
              :value="resources[`${langCode}_Table`].no"
              disabled
            />
          </div>
        </td>
      </tr>
    </thead>
    <tbody class="table-body" v-if="!isLoadingDish">
      <tr
        v-for="dish of dishs"
        :key="dish.dishID"
        :class="{ selected: dish.DishID === selectedDish.DishID }"
        @click="selectDish(dish)"
        @dblclick="handleDbClickDish(dish)"
        @contextmenu="handleRightClick(dish, $event)"
      >
        <td>{{ resources[`${langCode}_Table`].dish }}</td>
        <td>{{ dish.DishCode }}</td>
        <td>{{ dish.DishName }}</td>
        <td>{{ dish.MenuGroupName }}</td>
        <td>{{ dish.UnitName }}</td>
        <td align="right">{{ handlePrice(dish.Price) }}</td>
        <td>
          <base-icon iconName="checkbox-empty" />
        </td>
        <td><base-icon iconName="checkbox-empty" /></td>
        <td>{{ handleMaterialQuantified(dish.MaterialQuantified) }}</td>
        <td>
          <base-icon iconName="checkbox-empty" v-if="dish.ShowOnMenu === 0" />
          <base-icon iconName="checkbox-tick" v-if="dish.ShowOnMenu === 1" />
        </td>
        <td><base-icon iconName="checkbox-empty" /></td>
      </tr>
    </tbody>
    <div class="table-loading" v-if="isLoadingDish">
      <base-loading content="??ang l???y d??? li???u" />
    </div>
  </table>
  <!-- Dropdown ch???n ki???u t??m ki???m -->
  <div class="cbb-bg" @click="toggleDropdown" v-show="isShowDropdown"></div>
  <div
    class="cbb-option"
    v-show="isShowDropdown"
    :style="{
      top: `${dropdownTop}px`,
      left: `${dropdownLeft}px`,
    }"
    ref="comboboxOption"
  >
    <ul class="cbb-list-item">
      <li
        class="filter-item"
        :class="{ 'filter-selected': item.type === filterTypes[selectedCol] }"
        v-for="(item, i) of dropdownItem"
        :key="i"
        @click="handleSelectFilterType(item.type)"
      >
        <div>
          {{ item.text }}
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import enums from "@/enums";
import resources from "@/resources";
import { mapActions, mapState } from "vuex";

export default {
  data() {
    return {
      enums: enums,
      resources: resources,
      dropdownLeft: 0,
      dropdownTop: 0,
      isShowDropdown: false,
      selectedCol: null,
      dropdownItem: [],
      filterTypes: {
        DishName: enums.filterType.Contain,
        DishCode: enums.filterType.Contain,
        MenuGroupName: enums.filterType.Contain,
        UnitName: enums.filterType.Contain,
        Price: enums.filterType.LessOrEqual,
      },
      filterIcon: ["*", "=", "+", "-", "!", "<", "???", ">", "???"],
      filterObjects: [
        {
          columnName: "DishName",
          value: "",
          inputType: enums.inputType.Text,
          filterType: enums.filterType.Contain,
        },
        {
          columnName: "DishCode",
          value: "",
          inputType: enums.inputType.Text,
          filterType: enums.filterType.Contain,
        },
        {
          columnName: "MenuGroupName",
          value: "",
          inputType: enums.inputType.Text,
          filterType: enums.filterType.Contain,
        },
        {
          columnName: "UnitName",
          value: "",
          inputType: enums.inputType.Text,
          filterType: enums.filterType.Contain,
        },
        {
          columnName: "Price",
          value: "",
          inputType: enums.inputType.Number,
          filterType: enums.filterType.LessOrEqual,
        },
      ],
      sortObject: {
        sortBy: "",
        sortType: enums.sortType.Asc,
      },
    };
  },
  computed: mapState({
    langCode: (state) => state.app.langCode,
    dishs: (state) => state.dish.dishs,
    selectedDish: (state) => state.dish.selectedDish,
    isLoadingDish: (state) => state.dish.isLoadingDish,
  }),
  emits: ["showOptionMenu"],
  watch: {
    /**
     * Theo d??i s??? ki???n c???p nh???t filter object
     * @param newVal gi?? tr??? m???i
     * Author: linhpv (23/08/2022)
     */
    filterObjects(newVal) {
      console.log(newVal);
      this.updateFilterObjects(newVal);
      this.updatePageIndex(1);
      this.loadDishsByPaging();
    },
  },
  methods: {
    ...mapActions([
      "loadDishsByPaging",
      "selectDish",
      "updateFilterObjects",
      "updatePageIndex",
      "setFormMode",
      "toggleDishPopup",
      "updateSortInfo",
    ]),
    handleRightClick(dish, e) {
      // B??? x??? ki???n m???c ?????nh
      e.preventDefault();
      // Ch???n m??n ??n click v??o
      this.selectDish(dish);
      // Emit show option menu
      this.$emit("showOptionMenu", e);
    },
    /**
     * X??? l?? s???p x???p theo c???t
     * @param {*} col t??n c???t
     * Author: linhpv (24/08/2022)
     */
    handleSortData(col) {
      // Check xem c???t c?? ??ang sort hay kh??ng
      if (col === this.sortObject.sortBy) {
        // Thay ?????i ki???u sort
        this.sortObject.sortType =
          this.sortObject.sortType === enums.sortType.Asc
            ? enums.sortType.Desc
            : enums.sortType.Asc;
      } else {
        // Thay ?????i c???t sort
        this.sortObject = {
          sortBy: col,
          sortType: enums.sortType.Asc,
        };
      }

      // C???p nh???t tr???ng th??i s???p x???p
      this.updateSortInfo(this.sortObject);
      // G???i paging
      this.loadDishsByPaging();
    },
    /**
     * X??? l?? d??? li???u ?????u v??o khi ?? input l?? gi?? ti???n
     * @param {*} columnName t??n c???t
     * @param {*} value gi?? tr???
     * Created by: linhpv (24/08/2022)
     */
    handleFilterMoney(columnName, value) {
      // Format l???i value
      value = value.replaceAll(".", "");
      // Thay ?????i filter object
      this.handleChangeFilterObject(columnName, value);
    },
    /**
     * Ng?????i d??ng ch???n combobox l???c d??? li???u
     * @param {*} item Gi?? tr??? ??c ch???n
     * @param {*} col T??n c???t
     */
    selectTableCombobox(item, col) {
      // Map qua filter object ????? ki???m tra tr?????ng col ???? c?? ch??a
      let isFilter = false;
      // N???u r???i th?? s???a th??ng tin l???c
      this.filterObjects = this.filterObjects.map((filter) => {
        if (filter.columnName === col) {
          isFilter = true;
          return { ...filter, filterType: item[`${col}ID`] };
        }
        return filter;
      });
      // N???u ch??a th?? th??m m???i v??o m???ng l???c
      if (!isFilter) {
        this.filterObjects = [
          ...this.filterObjects,
          {
            columnName: col,
            value: "",
            inputType: enums.inputType.Boolean,
            filterType: item[`${col}ID`],
          },
        ];
      }
    },
    /**
     * X??? l?? ch???n ki???u l???c
     * @param {*} filterType ki???u l???c
     * Author: linhpv (23/08/2022)
     */
    handleSelectFilterType(filterType) {
      // Set filtertype cho c??c tr?????ng
      this.filterTypes[this.selectedCol] = filterType;
      // Set filter type trong filter object
      this.filterObjects = this.filterObjects.map((filter) => {
        if (filter.columnName === this.selectedCol) {
          return { ...filter, filterType };
        }
        return filter;
      });
      // ???n dropdown
      this.isShowDropdown = false;
    },
    /**
     * M??? popup ch???n ki???u l???c
     * @param {*} inputType ki???u d??? li???u ?????u v??o
     */
    openDropdown(inputType, columnName, e) {
      // Set c???t ??ang set l??
      this.selectedCol = columnName;
      // Set c??c item cho dropdown theo ki???u ?????u v??o
      if (inputType === enums.inputType.Text) {
        this.dropdownItem = [
          {
            type: enums.filterType.Contain,
            text: resources[`${this.langCode}_Filter_Type`].contain,
          },
          {
            type: enums.filterType.Equal,
            text: resources[`${this.langCode}_Filter_Type`].equal,
          },
          {
            type: enums.filterType.StartWith,
            text: resources[`${this.langCode}_Filter_Type`].startWith,
          },
          {
            type: enums.filterType.EndWith,
            text: resources[`${this.langCode}_Filter_Type`].endWith,
          },
          {
            type: enums.filterType.NotContain,
            text: resources[`${this.langCode}_Filter_Type`].notContain,
          },
        ];
      } else if (inputType === enums.inputType.Number) {
        this.dropdownItem = [
          {
            type: enums.filterType.Equal,
            text: resources[`${this.langCode}_Filter_Type`].equal,
          },
          {
            type: enums.filterType.Less,
            text: resources[`${this.langCode}_Filter_Type`].less,
          },
          {
            type: enums.filterType.LessOrEqual,
            text: resources[`${this.langCode}_Filter_Type`].lessOrEqual,
          },
          {
            type: enums.filterType.Greater,
            text: resources[`${this.langCode}_Filter_Type`].greater,
          },
          {
            type: enums.filterType.GreaterOrEqual,
            text: resources[`${this.langCode}_Filter_Type`].greaterOrEqual,
          },
        ];
      }
      // Set v??? tr?? dropdown
      let rect = e.target.getBoundingClientRect();
      this.dropdownLeft = rect.left;
      this.dropdownTop = rect.top + rect.height;
      // Hi???n th??? dropdown
      this.isShowDropdown = true;
    },
    /**
     * X??? l?? s??? ki???n ???n hi???n dropdown ch???n c??ch t??m ki???m
     * Author: linhpv (23/08/2022)
     */
    toggleDropdown() {
      this.isShowDropdown = !this.isShowDropdown;
    },
    /**
     * X??? l?? s??? ki???n double click v??o m??n ??n
     * Author: linhpv (15/08/2022)
     */
    handleDbClickDish(dish) {
      this.selectDish(dish);
      this.setFormMode(enums.formMode.Edit);
      this.toggleDishPopup();
    },
    /**
     * X??? l?? l???c
     * @param {*} columnName
     * @param {*} value
     * @param {*} inputType
     * @param {*} filterType
     * Author: linhpv (11/08/2022)
     */
    handleChangeFilterObject(columnName, value) {
      // Fomat l???i value
      value = value.trim();
      // Duy???t qua m???ng v?? c???p nh???t ph???n t???
      this.filterObjects = this.filterObjects.map((filter) => {
        if (filter.columnName === columnName) {
          return { ...filter, value };
        }
        return filter;
      });
    },
    /**
     * X??? l?? gi?? b??n
     * @param {string} val
     * Author: linhpv (09/08/2022)
     */
    handlePrice(val) {
      if (!val) val = 0;
      return val.toLocaleString("vi-VN", {
        minimumFractionDigits: 2,
        maximumFractionDigits: 2,
      });
    },
    /**
     * X??? l?? ?????nh l?????ng nguy??n v???t li???u
     * @param {string} val
     * Author: linhpv (09/08/2022)
     */
    handleMaterialQuantified(val) {
      switch (val) {
        case enums.yesNo.No:
          return resources[`${this.langCode}_Table`].notQuantified;
        case enums.yesNo.Yes:
          return resources[`${this.langCode}_Table`].quantified;
        default:
          return "";
      }
    },
  },
  created() {
    this.loadDishsByPaging();
  },
};
</script>

<style>
@import url(@/css/view/menu/menutable.css);
</style>