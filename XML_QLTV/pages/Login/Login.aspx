<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="XML_QLTV.pages.Login.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LoginStudents</title>
    <link href="https://fonts.googleapis.com/css2?family=Jost:wght@500&display=swap" rel="stylesheet">
    <link href="~/assets/css/style1.css" rel="stylesheet" />
</head>
<body>
    <section class="container">
        <div class="login-container">
            <div class="circle circle-one"></div>
            <div class="form-container">
                <img src="https://raw.githubusercontent.com/hicodersofficial/glassmorphism-login-form/master/assets/illustration.png" alt="illustration" class="illustration">
                <h1 class="opacity">LOGIN</h1>
                <form id="studentForm"> 
                    <input type="text" id="studentID" name="StudentID" placeholder="Mã sinh viên">
                    <input type="password" id="phoneNumber" name="PhoneNumber" placeholder="Mật Khẩu"><br />
                    <button type="submit" class="opacity">Đăng nhập</button> 
                    <p class="error" id="errorMessage"></p>
                </form>
                <div class="register-forget opacity">
                    <a href="/Account/DangKy">Đăng kí</a>&nbsp;&nbsp;&nbsp;
                    <a href="/Account/ForgotPassword">Quên mật khẩu</a>&nbsp;&nbsp;&nbsp;
                    <a href="LoginAdmin.aspx">Đăng nhập Cán Bộ</a>
                </div>
            </div>
            <div class="circle circle-two"></div>
            <div class="theme-btn-container"></div>
        </div>
    </section>

    <script>
        // Dữ liệu Students 
        const students = [
            { StudentID: "1", PhoneNumber: "0912345678" },
            { StudentID: "2", PhoneNumber: "0987654321" },
            { StudentID: "3", PhoneNumber: "0978123456" },
            { StudentID: "4", PhoneNumber: "0934561234" },
            { StudentID: "5", PhoneNumber: "0945678910" }
        ];

        // Xử lý sự kiện submit
        document.getElementById("studentForm").addEventListener("submit", function (event) {
            event.preventDefault(); // Ngăn chặn reload trang

            const studentID = document.getElementById("studentID").value.trim();
            const phoneNumber = document.getElementById("phoneNumber").value.trim();
            const errorMessage = document.getElementById("errorMessage");

            // Kiểm tra dữ liệu nhập
            const isValid = students.some(student => student.StudentID === studentID && student.PhoneNumber === phoneNumber);

            if (isValid) {
                // Chuyển đến trang index (Cập nhật đường dẫn cho chính xác)
                window.location.href = "../../library.xml";  // Đảm bảo đúng đường dẫn tới trang index.html
            } else {
                // Hiển thị lỗi nếu thông tin sai
                errorMessage.textContent = "Student ID hoặc Phone Number không đúng. Vui lòng thử lại!";
            }
        });
    </script>
</body>
</html>
