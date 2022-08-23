/**
 * Form mode
 * 0 - Thêm mới
 * 1 - Sửa
 */
const formMode = {
  Add: 0,
  Edit: 1,
};

/**
 * Có hoặc không
 * 0 - Không
 * 1 - Có
 */
const yesNo = {
  No: 0,
  Yes: 1,
};

/**
 * Kiểu dropdown
 * 0 - Kiểu danh sách
 * 1 - Kiểu bảng
 */
const dropdownType = {
  List: 0,
  Table: 1,
};

/**
 * Trạng thái cất
 * 0 - Cất
 * 1 - Cất và thêm
 */
const storeMode = {
  Store: 0,
  StoreAndAdd: 1,
};

export default {
  formMode,
  yesNo,
  dropdownType,
  storeMode,
};
