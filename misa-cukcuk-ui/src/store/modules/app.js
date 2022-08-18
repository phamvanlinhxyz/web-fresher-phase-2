const state = {
  langCode: "VN", // Mã ngôn ngữ
  isShowMenu: true, // Trạng thái hiển thị menu
  isShowDialog: false, // Trạng thái hiển thị dialog
};

const mutations = {
  TOGGLE_DIALOG(state) {
    state.isShowDialog = !state.isShowDialog;
  },
  TOGGLE_MENU(state) {
    state.isShowMenu = !state.isShowMenu;
  },
};

const actions = {
  toggleDialog(ctx) {
    ctx.commit("TOGGLE_DIALOG");
  },
  toggleMenu(ctx) {
    ctx.commit("TOGGLE_MENU");
  },
};

export default {
  state,
  mutations,
  actions,
};
