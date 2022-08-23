<template>
  <div style="padding: 20px">
    <h1>Button</h1>
    <base-button content="Đồng ý" type="1" />
    <br />
    <base-button content="Đồng ý" type="2" />
    <br />
    <base-button content="Đồng ý" type="1" icon="agree" />
    <br />
    <h1>Input</h1>
    <base-input type="text" v-model="testBind" />
    <br />
    <base-input type="text" disabled="true" />
    <br />
    <base-input
      type="text"
      errorMessage="Trường này không được phép để trống"
    />
    <br />
    <h1>Combobox</h1>
    <base-combobox :listItem="testData" tableName="Material" />
    <br />
    <base-combobox
      :listItem="testData"
      :addIcon="true"
      tableName="Material"
      disabled="true"
    />
    <br />
    <h1>Loading</h1>
    <div style="position: relative; width: 100%; height: 180px">
      <base-loading content="Đang lấy dữ liệu" />
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  computed: mapState({
    isShowMenu: (state) => state.app.isShowMenu,
  }),
  methods: {},
  data() {
    return {
      testData: [
        {
          MaterialID: 1,
          MaterialName: "Thịt bò",
        },
        {
          MaterialID: 2,
          MaterialName: "Thịt lợn",
        },
        {
          MaterialID: 3,
          MaterialName: "Thịt gà",
        },
      ],
      testBind: "Binding dữ liệu",
    };
  },
};
</script>