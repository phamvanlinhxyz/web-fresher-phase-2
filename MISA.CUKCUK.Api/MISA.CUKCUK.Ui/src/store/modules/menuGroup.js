import { constants } from "@/config";
import { handleError } from "@/utils";
import axios from "axios";

const state = {
  isShowMGPopup: false, // Trạng thái show popup thêm menu group
  menuGroups: [], // Tất cả nhóm thực đơn
};
const mutations = {
  INSERT_MENU_GROUP(state, payload) {
    state.menuGroups.unshift(payload);
    state.isShowMGPopup = false;
  },
  LOAD_ALL_MENU_GROUP(state, payload) {
    state.menuGroups = payload;
  },
  TOGGLE_MENU_GROUP_POPUP(state) {
    state.isShowMGPopup = !state.isShowMGPopup;
  },
};

const actions = {
  /**
   * Đóng mở popup thêm nhóm thực đơn
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  toggleMenuGroupPopup(ctx) {
    ctx.commit("TOGGLE_MENU_GROUP_POPUP");
  },
  /**
   * Thêm nhóm thực đơn mới
   * @param {*} ctx context
   * @param {object} menuGroup món ăn mới
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
    } else {
      handleError(ctx, res, menuGroup.MenuGroupName, menuGroup.MenuGroupCode);
    }
  },
  /**
   * Lấy tất cả nhóm thực đơn
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  async loadAllMenuGroup(ctx) {
    const res = await axios.get(
      `${constants.API_URL}/api/${constants.API_VERSION}/MenuGroup`
    );

    if (res.data.Success) {
      ctx.commit("LOAD_ALL_MENU_GROUP", res.data.Data);
    }
  },
};

export default {
  state,
  mutations,
  actions,
};
