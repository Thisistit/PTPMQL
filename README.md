Cấu trúc thư mục của dự án .Net MVC:
Dự án .NET MVC (ASP.NET Core MVC) được tổ chức theo mô hình MVC – Model, View, Controller, trong đó mỗi thư mục đảm nhiệm một vai trò riêng nhằm tách biệt rõ ràng giữa xử lý dữ liệu, xử lý logic và giao diện người dùng.

Thư mục Controllers chứa các lớp Controller, có nhiệm vụ tiếp nhận yêu cầu từ người dùng thông qua trình duyệt, xử lý logic nghiệp vụ và điều hướng dữ liệu sang View để hiển thị. Mỗi controller thường tương ứng với một nhóm chức năng trong hệ thống.

Thư mục Models dùng để định nghĩa các lớp mô hình dữ liệu. Các lớp này đại diện cho dữ liệu của hệ thống, thường ánh xạ với các bảng trong cơ sở dữ liệu, đồng thời được sử dụng để truyền dữ liệu giữa Controller và View.

Thư mục Views chứa các tập tin giao diện có phần mở rộng .cshtml. Mỗi Controller sẽ có một thư mục View riêng tương ứng. Trong Views còn có thư mục Shared, nơi lưu trữ các View dùng chung như layout (giao diện khung), trang lỗi hoặc các thành phần tái sử dụng cho nhiều View khác nhau.

Định tuyến (Route) trong .Net MVC:
Định tuyến (Routing) trong .NET MVC là cơ chế dùng để ánh xạ một URL mà người dùng nhập trên trình duyệt đến Controller và Action tương ứng trong ứng dụng. Nhờ có định tuyến, hệ thống biết được yêu cầu của người dùng cần được xử lý bởi controller nào và phương thức nào.Ngoài định tuyến mặc định, .NET MVC còn hỗ trợ định tuyến theo thuộc tính (Attribute Routing). Với cách này, route được khai báo trực tiếp trên controller hoặc action bằng các attribute. Cách này giúp kiểm soát URL rõ ràng và linh hoạt hơn.

Định tuyến đóng vai trò rất quan trọng trong .NET MVC vì nó quyết định cách người dùng truy cập vào các chức năng của hệ thống thông qua URL. Việc cấu hình định tuyến hợp lý giúp ứng dụng dễ sử dụng, dễ mở rộng và thân thiện với người dùng cũng như công cụ tìm kiếm.
