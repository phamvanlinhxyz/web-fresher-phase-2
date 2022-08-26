import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

import vClickOutside from "click-outside-vue3";
import BaseIcon from "./components/base/BaseIcon";
import BaseButton from "./components/base/BaseButton";
import BaseInput from "./components/base/BaseInput";
import BaseCombobox from "./components/base/BaseCombobox";
import BaseLoading from "./components/base/BaseLoading";
import BaseDialog from "./components/base/BaseDialog";

createApp(App)
  .component("base-icon", BaseIcon)
  .component("base-button", BaseButton)
  .component("base-input", BaseInput)
  .component("base-combobox", BaseCombobox)
  .component("base-loading", BaseLoading)
  .component("base-dialog", BaseDialog)
  .use(store)
  .use(router)
  .use(vClickOutside)
  .mount("#app");
