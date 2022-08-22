const state = {
  langCode: "VI", // Mã ngôn ngữ
  isShowMenu: true, // Trạng thái hiển thị menu
  isShowDialog: false, // Trạng thái hiển thị dialog
  dialogContent: null, // Nội dung hiển thị trong dialog
};

const mutations = {
  CHANGE_DIALOG_CONTENT(state, payload) {
    state.dialogContent = payload;
  },
  TOGGLE_DIALOG(state) {
    state.isShowDialog = !state.isShowDialog;
  },
  TOGGLE_MENU(state) {
    state.isShowMenu = !state.isShowMenu;
  },
};

const actions = {
  /**
   * Thay đổi nội dung dialog
   * @param {*} ctx context
   * @param {*} content nội dung dialog
   * Author: linhpv (19/08/2022)
   */
  changeDialogContent(ctx, content) {
    ctx.commit("CHANGE_DIALOG_CONTENT", content);
  },
  /**
   * Ẩn hiện dialog
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  toggleDialog(ctx) {
    ctx.commit("TOGGLE_DIALOG");
  },
  /**
   * Ẩn hiện menu
   * @param {*} ctx context
   * Author: linhpv (11/08/2022)
   */
  toggleMenu(ctx) {
    ctx.commit("TOGGLE_MENU");
  },
};

export default {
  state,
  mutations,
  actions,
};
