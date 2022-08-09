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
};

const mutations = {
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
};

const actions = {
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
    setTimeout(() => {
      ctx.commit("TOGGLE_LOADING");
    }, [1000]);
  },
};

export default {
  state,
  mutations,
  actions,
};
