import { constants } from "@/config";
import { handleError } from "@/utils";
import axios from "axios";

const state = {
  isShowKitchenPopup: false, // Trạng thái show popup thêm bếp
  kitchens: [], // Danh sách bếp
};

const mutations = {
  TOGGLE_KITCHEN_POPUP(state) {
    state.isShowKitchenPopup = !state.isShowKitchenPopup;
  },
  INSERT_KITCHEN(state, payload) {
    state.kitchens.unshift(payload);
    state.isShowKitchenPopup = false;
  },
  LOAD_ALL_KITCHEN(state, payload) {
    state.kitchens = payload;
  },
};

const actions = {
  /**
   * Thêm bếp mới
   * @param {*} ctx context
   * @param {*} kitchen bếp
   * Created by: linhpv (22/08/2022)
   */
  async insertKitchen(ctx, kitchen) {
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/Kitchen`,
      kitchen
    );

    if (res.data.Success) {
      kitchen.KitchenID = res.data.Data;
      ctx.commit("INSERT_KITCHEN", kitchen);
    } else {
      handleError(ctx, res, kitchen.KitchenName);
    }
  },
  /**
   * Đóng mở popup thêm bếp
   * @param {*} ctx context
   * Created by: linhpv (22/08/2022)
   */
  toggleKitchenPopup(ctx) {
    ctx.commit("TOGGLE_KITCHEN_POPUP");
  },
  /**
   * Lấy tất cả các bếp
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  async loadAllKitchen(ctx) {
    const res = await axios.get(
      `${constants.API_URL}/api/${constants.API_VERSION}/Kitchen`
    );

    if (res.data.Success) {
      ctx.commit("LOAD_ALL_KITCHEN", res.data.Data);
    }
  },
};

export default {
  state,
  mutations,
  actions,
};
