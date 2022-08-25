import { constants } from "@/config";
import { handleError } from "@/utils";
import axios from "axios";

const state = {
  isShowMaterialPopup: false, // Trạng thái show popup thêm nguyên vật liệu
  materials: [], // Danh sách nguyên vật liệu
};

const mutations = {
  INSERT_MATERIAL(state, payload) {
    state.materials.unshift(payload);
    state.isShowMaterialPopup = false;
  },
  TOGGLE_MATERIAL_POPUP(state) {
    state.isShowMaterialPopup = !state.isShowMaterialPopup;
  },
  LOAD_ALL_MATERIAL(state, payload) {
    state.materials = payload;
  },
};

const actions = {
  /**
   * Thêm nguyên vật liệu mới
   * @param {*} ctx  context
   * @param {object} material nguyên vật liệu mới
   * Author: linhpv (16/08/2022)
   */
  async insertMaterial(ctx, material) {
    const res = await axios.post(
      `${constants.API_URL}/api/${constants.API_VERSION}/Material`,
      material
    );

    if (res.data.Success) {
      material.MaterialID = res.data.Data;
      ctx.commit("INSERT_MATERIAL", material);
    } else {
      handleError(ctx, res, material.MaterialName, material.MaterialCode);
    }
  },
  /**
   * Đóng, mở popup thêm nguyên vật liệu
   * @param {*} ctx context
   * Author: linhpv (16/08/2022)
   */
  toggleMaterialPopup(ctx) {
    ctx.commit("TOGGLE_MATERIAL_POPUP");
  },
  /**
   * Lấy tất cả nguyên vật liệu
   * @param {*} ctx context
   * Author: linhpv (16/08/2022)
   */
  async loadAllMaterial(ctx) {
    const res = await axios.get(
      `${constants.API_URL}/api/${constants.API_VERSION}/Material`
    );

    if (res.data.Success) {
      ctx.commit("LOAD_ALL_MATERIAL", res.data.Data);
    }
  },
};

export default {
  state,
  mutations,
  actions,
};
