import { createStore } from "vuex";
import app from "./modules/app";
import dish from "./modules/dish";

export default createStore({
  modules: {
    app,
    dish,
  },
});
