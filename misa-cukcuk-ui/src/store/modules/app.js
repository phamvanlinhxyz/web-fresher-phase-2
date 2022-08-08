const state = {
  isShowMenu: true,
};

const mutations = {
  toggleMenu(state) {
    state.isShowMenu = !state.isShowMenu;
  },
};

const actions = {
  toggleMenu(ctx) {
    ctx.commit("toggleMenu");
  },
};

export default {
  state,
  mutations,
  actions,
};
