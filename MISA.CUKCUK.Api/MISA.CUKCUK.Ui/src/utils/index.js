import enums from "@/enums";
import resources from "@/resources";
import app from "@/store/modules/app";

/**
 * Khiểm tra type của object
 * @param {object?} object
 * Author: linhpv (19/08/2022)
 */
const isObject = (object) => {
  return object != null && typeof object === "object";
};

/**
 * So sánh 2 object
 * Author: linhpv (19/08/2022)
 */
export const objectEqual = (object1, object2) => {
  try {
    const keys1 = Object.keys(object1);
    const keys2 = Object.keys(object2);
    // Check số lượng key của 2 object
    if (keys1.length !== keys2.length) {
      return false;
    }
    // Check lần lượt các value
    for (const key of keys1) {
      const val1 = object1[key];
      const val2 = object2[key];
      const areObjects = isObject(val1) && isObject(val2);
      if (
        (areObjects && !this.objectEqual(val1, val2)) ||
        (!areObjects && val1 !== val2)
      ) {
        return false;
      }
    }
    return true;
  } catch (error) {
    console.log(error);
  }
};

/**
 * Xử lý lỗi trả về
 * @param {*} ctx context
 * @param {*} res response trả về
 * Author: linhpv (19/08/2022)
 */
export const handleError = (ctx, res, name, code, unit) => {
  const langCode = app.state.langCode;

  // Lấy message
  var msg = "";

  switch (res.data.ErrorCode) {
    case enums.errorCode.EmptyName:
      msg = resources[`${langCode}_Error_Msg`].emptyName;
      break;
    case enums.errorCode.EmptyCode:
      msg = resources[`${langCode}_Error_Msg`].emptyCode;
      break;
    case enums.errorCode.EmptyUnit:
      msg = resources[`${langCode}_Error_Msg`].emptyUnit;
      break;
    case enums.errorCode.EmptyPrice:
      msg = resources[`${langCode}_Error_Msg`].emptyPrice;
      break;
    case enums.errorCode.DuplicateName:
      msg = resources[`${langCode}_Error_Msg`].duplicateName(name);
      break;
    case enums.errorCode.DuplicateCode:
      msg = resources[`${langCode}_Error_Msg`].duplicateCode(code);
      break;
    case enums.errorCode.AddFailed:
      msg = resources[`${langCode}_Error_Msg`].addFailed;
      break;
    case enums.errorCode.EditFailed:
      msg = resources[`${langCode}_Error_Msg`].editFailed;
      break;
    case enums.errorCode.OverSize:
      msg = resources[`${langCode}_Error_Msg`].overSize;
      break;
    case enums.errorCode.DuplicateUnit:
      msg = resources[`${langCode}_Error_Msg`].duplicateUnit(unit);
      break;
    default:
      msg = resources[`${langCode}_Error_Msg`].serverInternal;
  }

  // Commit dialog
  ctx.commit("CHANGE_DIALOG_CONTENT", msg);
  ctx.commit("TOGGLE_DIALOG");
};
