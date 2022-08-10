import { constants } from "@/config";
import axios from "axios";

const state = {
  dishs: [],
  pageSize: 100,
  pageIndex: 1,
  filterObjects: [],
  isLoadingDish: false,
  totalRecord: 0,
  totalPage: 0,
  isShowDishPopup: false,
};

const mutations = {
  TOGGLE_DISH_POPUP(state) {
    state.isShowDishPopup = !state.isShowDishPopup
  },
  LOAD_ALL_DISH(state, payload) {
    state.dishs = payload;
  },
  LOAD_DISHS_BY_PAGING(state, payload) {
    state.dishs = payload.data;
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

      ctx.commit("LOAD_ALL_DISH", res.data);
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
    try {
      const res = await axios.post(
        `${constants.API_URL}/api/${constants.API_VERSION}/Dish/paging?pageIndex=${state.pageIndex}&pageSize=${state.pageSize}`,
        state.filterObjects
      );

      ctx.commit("LOAD_DISHS_BY_PAGING", res.data);
    } catch (error) {
      console.log(error);
    }
    ctx.commit("TOGGLE_LOADING");
  },
};

export default {
  state,
  mutations,
  actions,
};
