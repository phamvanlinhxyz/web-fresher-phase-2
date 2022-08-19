<template>
  <table class="table">
    <thead class="table-header">
      <tr>
        <td style="min-width: 159px">
          <div class="table-column">Loại món</div>
          <div class="table-filter">
            <input class="input" value="Món ăn" disabled />
          </div>
        </td>
        <td style="min-width: 179px">
          <div class="table-column">Mã món</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('DishCode', $event.target.value, 0, 0)
              "
            />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Tên món</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('DishName', $event.target.value, 0, 0)
              "
            />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Nhóm thực đơn</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input
              class="input"
              @change="
                handleChangeFilterObject(
                  'MenuGroupName',
                  $event.target.value,
                  0,
                  0
                )
              "
            />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column">Đơn vị tính</div>
          <div class="table-filter">
            <div class="filter-type">*</div>
            <input
              class="input"
              @change="
                handleChangeFilterObject('UnitName', $event.target.value, 0, 0)
              "
            />
          </div>
        </td>
        <td style="min-width: 119px">
          <div class="table-column">Giá bán</div>
          <div class="table-filter">
            <div class="filter-type">≤</div>
            <input
              class="input"
              type="number"
              style="text-align: right"
              @change="
                handleChangeFilterObject('Price', $event.target.value, 1, 6)
              "
            />
          </div>
        </td>
        <td style="min-width: 150px">
          <div class="table-column">Thay đổi theo thời giá</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Điều chỉnh tự do</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 139px">
          <div class="table-column">Định lượng NVL</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 149px">
          <div class="table-column">Hiển thị trên thực đơn</div>
          <div class="table-filter">
            <input class="input" />
          </div>
        </td>
        <td style="min-width: 89px">
          <div class="table-column">Ngừng bán</div>
          <div class="table-filter">
            <input class="input" value="Không" />
          </div>
        </td>
      </tr>
    </thead>
    <tbody class="table-body" v-if="!isLoadingDish">
      <tr
        v-for="dish of dishs"
        key="dish.dishID"
        :class="{ selected: dish.DishID === selectedDish.DishID }"
        @click="selectDish(dish)"
        @dblclick="handleDbClickDish(dish)"
      >
        <td>Món ăn</td>
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
      <base-loading content="Đang lấy dữ liệu" />
    </div>
  </table>
 
</template>

<script>
import enums from "@/enums";
import { mapActions, mapState } from "vuex";

export default {
  computed: mapState({
    dishs: (state) => state.dish.dishs,
    selectedDish: (state) => state.dish.selectedDish,
    isLoadingDish: (state) => state.dish.isLoadingDish,
    filterObjects: (state) => state.dish.filterObjects,
  }),
  methods: {
    ...mapActions([
      "loadDishsByPaging",
      "selectDish",
      "updateFilterObjects",
      "updatePageIndex",
      "setFormMode",
      "toggleDishPopup",
    ]),
    /**
     * Xử lý sự kiện double click vào món ăn
     * Author: linhpv (15/08/2022)
     */
    handleDbClickDish(dish) {
      this.selectDish(dish);
      this.setFormMode(enums.formMode.Edit);
      this.toggleDishPopup();
    },
    /**
     * Xử lý lọc
     * @param {*} columnName
     * @param {*} value
     * @param {*} inputType
     * @param {*} filterType
     * Author: linhpv (11/08/2022)
     */
    handleChangeFilterObject(columnName, value, inputType, filterType) {
      // Tìm vị trí của object trong mảng lọc
      var objectIndex = this.filterObjects.findIndex((object) => {
        return object.columnName === columnName;
      });

      var newObjects = [];

      if (objectIndex === -1) {
        newObjects = [
          ...this.filterObjects,
          {
            columnName,
            value,
            inputType,
            filterType,
          },
        ];
      } else {
        newObjects = this.filterObjects;
        newObjects[objectIndex] = {
          columnName,
          value,
          inputType,
          filterType,
        };
      }
      this.updateFilterObjects(newObjects);
      this.updatePageIndex(1);
      this.loadDishsByPaging();
    },
    /**
     * Xử lý giá bán
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
     * Xử lý định lượng nguyên vật liệu
     * @param {string} val
     * Author: linhpv (09/08/2022)
     */
    handleMaterialQuantified(val) {
      switch (val) {
        case enums.yesNo.No:
          return "Chưa thiết lập";
        case enums.yesNo.Yes:
          return "Đã thiết lập";
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