import { constants } from "@/config";
import enums from "@/enums";
import resources from "@/resources";
import axios from "axios";

const state = {
  dishs: [], // Danh sách món ăn theo phân trang
  selectedDish: {}, // Món ăn được chọn
  pageSize: 100, // Số lượng món ăn trên 1 trang
  pageIndex: 1, // Số trang
  filterObjects: [], // Object các trường lọc
  isLoadingDish: false, // Trạng thái đang load món ăn
  totalRecord: 0, // Tổng số món ăn
  totalPage: 0, // Tổng số trang
  isShowDishPopup: false, // Trạng thái show popup thêm sửa món ăn
  isShowMGPopup: false, // Trang thái show popup thêm nhóm thực đơn
  isShowUnitPopup: false, // Trạng thái show popup thêm đơn vị tính
  isShowMaterialPopup: false, // Trạng thái show popup thêm nguyên vật liệu
  isShowKitchenPopup: false, // Trạng thái show popup thêm bếp
  formMode: enums.formMode.Add, // Mode của form
  menuGroups: [], // Danh sách nhóm thực đơn
  units: [], // Danh sách đơn vị tính
  kitchens: [], // Danh sách bếp
  materials: [], // Danh sách nguyên vật liệu
};

const mutations = {
  TOGGLE_KITCHEN_POPUP(state) {
    state.isShowKitchenPopup = !state.isShowKitchenPopup;
  },
  INSERT_KITCHEN(state, payload) {
    state.kitchens.unshift(payload);
    state.isShowKitchenPopup = false;
  },
  UPDATE_DISH(state, payload) {
    state.dishs = state.dishs.map((dish) => {
      if (dish.DishID == payload.DishID) {
        return payload;
      }
      return dish;
    });
    state.isShowDishPopup = false;
  },
  INSERT_MATERIAL(state, payload) {
    state.materials.unshift(payload);
    state.isShowMaterialPopup = false;
  },
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
  TOGGLE_MATERIAL_POPUP(state) {
    state.isShowMaterialPopup = !state.isShowMaterialPopup;
  },
  TOGGLE_UNIT_POPUP(state) {
    state.isShowUnitPopup = !state.isShowUnitPopup;
  },
  TOGGLE_MG_POPUP(state) {
    state.isShowMGPopup = !state.isShowMGPopup;
  },
  LOAD_ALL_MATERIAL(state, payload) {
    state.materials = payload;
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
      handleError(ctx, res);
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
   * Cập nhật thông tin món ăn
   * @param {*} ctx context
   * @param {object} dish món ăn
   * Author: linhpv (18/08/2022)
   */
  async updateDish(ctx, dish) {
    const res = await axios.put(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish`,
      dish
    );

    if (res.data.Success) {
      ctx.commit("UPDATE_DISH", dish);
    } else {
      handleError(ctx, res);
    }
  },
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
      handleError(ctx, res);
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
      handleError(ctx, res);
    }
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
      handleError(ctx, res);
    }
  },
  /**
   * Thay đổi form mode
   * @param {*} ctx context
   * @param {int} formMode form mode mới
   * Author: linhpv (15/08/2022)
   */
  setFormMode(ctx, formMode) {
    ctx.commit("SET_FORM_MODE", formMode);
  },
  /**
   * Thêm món ăn mới
   * @param {*} ctx context
   * @param {object} dish món ăn mới
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
    } else {
      handleError(ctx, res);
    }
  },
  /**
   * Xóa món ăn
   * @param {*} ctx context
   * @param {string} dishID id món ăn
   * Author: linhpv (11/08/2022)
   */
  async deleteDish(ctx, dishID) {
    var res = await axios.delete(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish/${dishID}`
    );

    if (res.data.Success) {
      ctx.commit("DELETE_DISH", dishID);
    } else {
      handleError(ctx, res);
    }
  },
  /**
   * Chọn món ăn
   * @param {*} ctx context
   * @param {object} dish món ăn
   * Author: linhpv (11/08/2022)
   */
  selectDish(ctx, dish) {
    ctx.commit("SELECT_DISH", dish);
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
   * Đóng mở popup thêm nhóm thực đơn
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  toggleMenuGroupPopup(ctx) {
    ctx.commit("TOGGLE_MG_POPUP");
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
  /**
   * Đóng mở popup thêm món ăn
   * @param {*} ctx context
   * Author: linhpv (10/08/2022)
   */
  toggleDishPopup(ctx) {
    ctx.commit("TOGGLE_DISH_POPUP");
  },
  /**
   * Cập nhật filter object
   * @param {*} ctx context
   * @param {object} filterObjects object lọc
   * Author: linhpv (10/08/2022)
   */
  updateFilterObjects(ctx, filterObjects) {
    ctx.commit("UPDATE_FILTER_OBJECTS", filterObjects);
  },
  /**
   * Cập nhật số bản ghi trên 1 trang
   * @param {*} ctx context
   * @param {int} pageSize số bản ghi trên 1 trang
   * Author: linhpv (10/08/2022)
   */
  updatePageSize(ctx, pageSize) {
    ctx.commit("UPDATE_PAGE_SIZE", pageSize);
  },
  /**
   * Cập nhật số trang
   * @param {*} ctx context
   * @param {int} pageIndex số trang
   * Author: linhpv (10/08/2022)
   */
  updatePageIndex(ctx, pageIndex) {
    ctx.commit("UPDATE_PAGE_INDEX", pageIndex);
  },
  /**
   * Lấy tất cả món ăn trong db
   * @param {*} ctx context
   * Author: linhpv (09/08/2022)
   */
  async loadAllDish(ctx) {
    const res = await axios.get(
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish`
    );

    if (res.data.Data) {
      ctx.commit("LOAD_ALL_DISH", res.data.Data);
    }
  },
  /**
   * Lấy dữ liệu theo phân trang tìm kiếm,...
   * @param {*} ctx context
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
    } else {
      handleError(ctx, res);
    }

    ctx.commit("TOGGLE_LOADING");
  },
};

/**
 * Xử lý lỗi trả về
 * @param {*} ctx context
 * @param {*} res response trả về
 * Author: linhpv (19/08/2022)
 */
const handleError = (ctx, res) => {
  let msg = res.data.UserMsg;
  // Nếu msg trả về trống => hiển thị thông báo đã có lỗi
  if (!msg || msg == "") {
    msg = resources.VN_Error_Msg;
  }

  // Commit dialog
  ctx.commit(
    "CHANGE_DIALOG_CONTENT",
    msg.split(";").map((m) => m.trim())
  );
  ctx.commit("TOGGLE_DIALOG");
};

export default {
  state,
  mutations,
  actions,
};
