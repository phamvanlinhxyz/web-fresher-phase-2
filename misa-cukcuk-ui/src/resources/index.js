const VN_Required_Error = "Trường này không được để trống.";
const VN_Data_Changed = "Dữ liệu đã thay đổi, bạn có muốn cất không?";
const VN_Error_Msg = "Đã có lỗi xảy ra vui lòng liên hệ MISA để được hỗ trợ.";

const VN_Confirm_Delete = (dishCode, dishName) => {
  return `Bạn có chắc chắn muốn xóa món <${dishCode} - ${dishName}> không?`;
};

export default {
  VN_Required_Error,
  VN_Data_Changed,
  VN_Error_Msg,
  VN_Confirm_Delete,
};
