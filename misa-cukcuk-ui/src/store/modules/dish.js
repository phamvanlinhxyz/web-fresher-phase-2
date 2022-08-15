import { constants } from "@/config";
import enums from "@/enums";
import axios from "axios";

const state = {
  dishs: [],
  selectedDish: {},
  pageSize: 100,
  pageIndex: 1,
  filterObjects: [],
  isLoadingDish: false,
  totalRecord: 0,
  totalPage: 0,
  isShowDishPopup: false,
  isShowMGPopup: false,
  isShowUnitPopup: false,
  formMode: enums.formMode.Add,
  menuGroups: [],
  units: [],
  kitchens: [],
};

const mutations = {
  INSERT_UNIT(state, payload) {
    state.units.unshift(payload);
    state.selectedDish.UnitID = payload.UnitID;
    state.isShowUnitPopup = false;
  },
  INSERT_MENU_GROUP(state, payload) {
    state.menuGroups.unshift(payload);
    state.selectedDish.MenuGroupID = payload.MenuGroupID;
    state.isShowMGPopup = false;
  },
  SET_FORM_MODE(state, payload) {
    state.formMode = payload;
  },
  INSERT_DISH(state, payload) {
    state.dishs.unshift(payload);
    state.totalRecord++;
    state.isShowDishPopup = false;
  },
  DELETE_DISH(state, payload) {
    state.dishs = state.dishs.filter((dish) => dish.DishID !== payload);
    state.totalRecord--;
  },
  SELECT_DISH(state, payload) {
    state.selectedDish = payload;
  },
  TOGGLE_UNIT_POPUP(state) {
    state.isShowUnitPopup = !state.isShowUnitPopup;
  },
  TOGGLE_MG_POPUP(state) {
    state.isShowMGPopup = !state.isShowMGPopup;
  },
  LOAD_ALL_KITCHEN(state, payload) {
    state.kitchens = payload;
  },
  LOAD_ALL_UNIT(state, payload) {
    state.units = payload;
  },
  LOAD_ALL_MENU_GROUP(state, payload) {
    state.menuGroups = payload;
  },
  TOGGLE_DISH_POPUP(state) {
    state.isShowDishPopup = !state.isShowDishPopup;
  },
  LOAD_ALL_DISH(state, payload) {
    state.dishs = payload;
  },
  LOAD_DISHS_BY_PAGING(state, payload) {
    state.dishs = payload.data;
    state.selectedDish = payload.data[0];
    state.totalRecord = payload.totalRecord;
    state.totalPage = payload.totalPage;
  },
  TOGGLE_LOADING(state) {
    state.isLoadingDish = !state.isLoadingDish;
  },
  UPDATE_PAGE_INDEX(state, payload) {
    state.pageIndex = payload;
  },
  UPDATE_PAGE_SIZE(state, payload) {
    state.pageSize = payload;
  },
  UPDATE_FILTER_OBJECTS(state, payload) {
    state.filterObjects = payload;
  },
};

const actions = {
  /**
   * Thêm đơn vị tính mới
   * @param {*} ctx context
   * @param {*} unit đơn vị tính
   * Author: linhpv (15/08/2022)
   */
  async insertUnit(ctx, unit) {
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/Unit`,
      unit
    );

    if (res.data.Success) {
      unit.UnitID = res.data.Data;
      ctx.commit("INSERT_UNIT", unit);
    }
  },
  /**
   * Thêm nhóm thực đơn mới
   * @param {*} ctx context
   * @param {*} menuGroup món ăn mới
   * Author: linhpv (15/08/2022)
   */
  async insertMenuGroup(ctx, menuGroup) {
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/MenuGroup`,
      menuGroup
    );

    if (res.data.Success) {
      menuGroup.MenuGroupID = res.data.Data;
      ctx.commit("INSERT_MENU_GROUP", menuGroup);
    }
  },
  /**
   * Thay đổi form mode
   * @param {*} ctx context
   * @param {*} formMode form mode mới
   * Author: linhpv (15/08/2022)
   */
  setFormMode(ctx, formMode) {
    ctx.commit("SET_FORM_MODE", formMode);
  },
  /**
   * Thêm món ăn mới
   * @param {*} ctx context
   * @param {*} dish món ăn mới
   * Author: linhpv (14/08/2022)
   */
  async insertDish(ctx, dish) {
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish`,
      dish
    );

    if (res.data.Success) {
      dish.DishID = res.data.Data;
      ctx.commit("INSERT_DISH", dish);
    }
  },
  /**
   * Xóa món ăn
   * @param {*} ctx context
   * @param {*} dishID id món ăn
   * Author: linhpv (11/08/2022)
   */
  async deleteDish(ctx, dishID) {
    var res = await axios.delete(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish/${dishID}`
    );

    if (res.data.Success) {
      ctx.commit("DELETE_DISH", dishID);
    }
  },
  /**
   * Chọn món ăn
   * @param {*} ctx
   * @param {*} dish
   * Author: linhpv (11/08/2022)
   */
  selectDish(ctx, dish) {
    ctx.commit("SELECT_DISH", dish);
  },
  /**
   * Đóng mở popup thêm đơn vị tính
   * @param {*} ctx
   * Author: linhpv (11/08/2022)
   */
  toggleUnitPopup(ctx) {
    ctx.commit("TOGGLE_UNIT_POPUP");
  },
  /**
   * Đóng mở popup thêm nhóm thực đơn
   * @param {*} ctx
   * Author: linhpv (11/08/2022)
   */
  toggleMenuGroupPopup(ctx) {
    ctx.commit("TOGGLE_MG_POPUP");
  },
  /**
   * Lấy tất cả các bếp
   * @param {*} ctx
   * Author: linhpv (11/08/2022)
   */
  async loadAllKitchen(ctx) {
    try {
      const res = await axios.get(
        `${constants.API_URL}/api/${constants.API_VERSION}/Kitchen`
      );

      ctx.commit("LOAD_ALL_KITCHEN", res.data.Data);
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * Lấy tất cả đơn vị tính
   * @param {*} ctx
   * Author: linhpv (11/08/2022)
   */
  async loadAllUnit(ctx) {
    try {
      const res = await axios.get(
        `${constants.API_URL}/api/${constants.API_VERSION}/Unit`
      );

      ctx.commit("LOAD_ALL_UNIT", res.data.Data);
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * Lấy tất cả nhóm thực đơn
   * @param {*} ctx
   * Author: linhpv (11/08/2022)
   */
  async loadAllMenuGroup(ctx) {
    try {
      const res = await axios.get(
        `${constants.API_URL}/api/${constants.API_VERSION}/MenuGroup`
      );

      ctx.commit("LOAD_ALL_MENU_GROUP", res.data.Data);
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * Đóng mở popup thêm món ăn
   * @param {*} ctx
   * Author: linhpv (10/08/2022)
   */
  toggleDishPopup(ctx) {
    ctx.commit("TOGGLE_DISH_POPUP");
  },
  /**
   * Cập nhật filter object
   * @param {*} ctx
   * @param {*} filterObjects
   * Author: linhpv (10/08/2022)
   */
  updateFilterObjects(ctx, filterObjects) {
    ctx.commit("UPDATE_FILTER_OBJECTS", filterObjects);
  },
  /**
   * Cập nhật số bản ghi trên 1 trang
   * @param {*} ctx
   * @param {*} pageSize
   * Author: linhpv (10/08/2022)
   */
  updatePageSize(ctx, pageSize) {
    ctx.commit("UPDATE_PAGE_SIZE", pageSize);
  },
  /**
   * Cập nhật số trang
   * @param {*} ctx
   * @param {*} pageIndex
   * Author: linhpv (10/08/2022)
   */
  updatePageIndex(ctx, pageIndex) {
    ctx.commit("UPDATE_PAGE_INDEX", pageIndex);
  },
  /**
   * Lấy tất cả món ăn trong db
   * @param {*} ctx
   * Author: linhpv (09/08/2022)
   */
  async loadAllDish(ctx) {
    try {
      const res = await axios.get(
        `${constants.API_URL}/api/${constants.API_VERSION}/Dish`
      );

      ctx.commit("LOAD_ALL_DISH", res.data.Data);
    } catch (error) {
      console.log(error);
    }
  },
  /**
   * Lấy dữ liệu theo phân trang tìm kiếm,...
   * @param {*} ctx
   * Author: linhpv (09/08/2022)
   */
  async loadDishsByPaging(ctx) {
    ctx.commit("TOGGLE_LOADING");
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish/Paging?pageIndex=${state.pageIndex}&pageSize=${state.pageSize}`,
      state.filterObjects
    );

    if (res.data.Success) {
      ctx.commit("LOAD_DISHS_BY_PAGING", res.data.Data);
    }

    ctx.commit("TOGGLE_LOADING");
  },
};

export default {
  state,
  mutations,
  actions,
};
