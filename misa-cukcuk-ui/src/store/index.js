import { createStore } from "vuex";
import app from "./modules/app";
import dish from "./modules/dish";
import menuGroup from "./modules/menuGroup";
import unit from "./modules/unit";
import kitchen from "./modules/kitchen";
import material from "./modules/material";

export default createStore({
  modules: {
    app,
    dish,
    menuGroup,
    unit,
    kitchen,
    material,
  },
});
