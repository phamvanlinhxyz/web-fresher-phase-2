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
