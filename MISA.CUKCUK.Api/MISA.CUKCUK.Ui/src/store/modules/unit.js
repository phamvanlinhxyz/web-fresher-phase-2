import { constants } from "@/config";
import enums from "@/enums";
import resources from "@/resources";
import { handleError } from "@/utils";
import axios from "axios";
import app from "./app";

const state = {
  isShowUnitPopup: false, // Trạng thái show popup thêm đơn vị tính
  units: [], // Danh sách đơn vị tính
};

const mutations = {
  INSERT_UNIT(state, payload) {
    state.units.unshift(payload);
    state.isShowUnitPopup = false;
  },
  TOGGLE_UNIT_POPUP(state) {
    state.isShowUnitPopup = !state.isShowUnitPopup;
  },
  LOAD_ALL_UNIT(state, payload) {
    state.units = payload;
  },
};

const actions = {
  /**
   * Thêm đơn vị tính mới
   * @param {*} ctx context
   * @param {object} unit đơn vị tính
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
    } else {
      handleError(ctx, res, "", "", unit.UnitName);
    }
  },
  /**
   * Đóng mở popup thêm đơn vị tính
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  toggleUnitPopup(ctx) {
    ctx.commit("TOGGLE_UNIT_POPUP");
  },
  /**
   * Lấy tất cả đơn vị tính
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  async loadAllUnit(ctx) {
    const res = await axios.get(
      `${constants.API_URL}/api/${constants.API_VERSION}/Unit`
    );

    if (res.data.Success) {
      ctx.commit("LOAD_ALL_UNIT", res.data.Data);
    }
  },
};

export default {
  state,
  mutations,
  actions,
};
