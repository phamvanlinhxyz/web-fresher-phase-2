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

/**
 * Kiểu lọc
 */
const filterType = {
  Contain: 0, // Chứa
  Equal: 1, // Bằng
  StartWith: 2, // Bắt đầu bằng
  EndWith: 3, // Kết thúc bằng
  NotContain: 4, // Không chứa
  Less: 5, // Nhỏ hơn
  LessOrEqual: 6, // Nhỏ hơn hoặc bằng
  Greater: 7, // Lớn hơn
  GreaterOrEqual: 8, // Lớn hơn hoặc bằng
  True: 9, // Giá trị đúng
  False: 10, // Giá trị sai
};

/**
 * Kiểu dữ liệu đầu vào
 */
const inputType = {
  Text: 0, // Kiểu chữ
  Number: 1, // Kiểu số
  Boolean: 2, // Kiểm đúng sai
};

/**
 * Kiểu sắp xếp
 */
const sortType = {
  Asc: "ASC", // Tăng dần
  Desc: "DESC", // Giảm dần
};

export default {
  formMode,
  yesNo,
  dropdownType,
  storeMode,
  filterType,
  inputType,
  sortType,
};
