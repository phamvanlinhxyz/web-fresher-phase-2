const VN_Required_Error = "Trường này không được để trống.";

const VN_Confirm_Delete = (dishCode, dishName) => {
  return `Bạn có chắc chắn muốn xóa món <${dishCode} - ${dishName}> không?`;
};

export default {
  VN_Required_Error,
  VN_Confirm_Delete,
};
