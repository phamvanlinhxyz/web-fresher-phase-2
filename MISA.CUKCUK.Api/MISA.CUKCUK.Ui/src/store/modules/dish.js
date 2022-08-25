import { constants } from "@/config";
import enums from "@/enums";
import resources from "@/resources";
import { handleError } from "@/utils";
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
  sortBy: "", // Sắp xếp theo trường nào
  sortType: "", // Sắp xếp theo kiểu gì
  isShowDishPopup: false, // Trạng thái show popup thêm sửa món ăn
  formMode: enums.formMode.Add, // Mode của form
  storeMode: enums.storeMode.Store, // Mode cất
};

const mutations = {
  UPDATE_SORT_INFO(state, payload) {
    state.sortBy = payload.sortBy;
    state.sortType = payload.sortType;
  },
  UPDATE_DISH(state, payload) {
    state.dishs = state.dishs.map((dish) => {
      if (dish.DishID == payload.DishID) {
        return payload;
      }
      return dish;
    });
    if (state.storeMode == enums.storeMode.Store) {
      state.isShowDishPopup = false;
    } else {
      state.formMode = enums.formMode.Add;
      state.selectedDish = {
        ShowOnMenu: enums.yesNo.Yes,
        SemiFinishedProduct: enums.yesNo.No,
        Price: 0,
        PurchasePrice: 0,
      };
    }
  },
  SET_STORE_MODE(state, payload) {
    state.storeMode = payload;
  },
  SET_FORM_MODE(state, payload) {
    state.formMode = payload;
  },
  INSERT_DISH(state, payload) {
    state.dishs.unshift(payload);
    state.totalRecord++;
    if (state.storeMode == enums.storeMode.Store) {
      state.isShowDishPopup = false;
    } else {
      state.selectedDish = {
        ShowOnMenu: enums.yesNo.Yes,
        SemiFinishedProduct: enums.yesNo.No,
        Price: 0,
        PurchasePrice: 0,
      };
    }
  },
  DELETE_DISH(state, payload) {
    state.dishs = state.dishs.filter((dish) => dish.DishID !== payload);
    state.totalRecord--;
  },
  SELECT_DISH(state, payload) {
    state.selectedDish = payload;
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
   * Cập nhật thông tin sắp xếp
   * @param {*} ctx context
   * @param {*} sortInfo thông tin sắp xếp
   * Created by: linhpv (24/08/2022)
   */
  updateSortInfo(ctx, sortInfo) {
    ctx.commit("UPDATE_SORT_INFO", sortInfo);
  },
  /**
   * Cập nhật tùy chọn cất
   * @param {*} ctx context
   * @param {*} mode tùy chọn
   * Author: linhpv (22/08/2022)
   */
  setStoreMode(ctx, mode) {
    ctx.commit("SET_STORE_MODE", mode);
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
      handleError(ctx, res, dish.DishName, dish.DishCode, "");
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
      handleError(ctx, res, dish.DishName, dish.DishCode, "");
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
      `${constants.API_URL}/api/${constants.API_VERSION}/Dish/Paging?pageIndex=${state.pageIndex}&pageSize=${state.pageSize}&sortBy=${state.sortBy}&sortType=${state.sortType}`,
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

export default {
  state,
  mutations,
  actions,
};
