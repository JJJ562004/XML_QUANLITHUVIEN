<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categories.aspx.cs" Inherits="XML_QLTV.pages.books.authors" EnableViewState="true" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Star Admin2 </title>
    <!-- plugins:css -->
    <link rel="stylesheet" href="../../assets/vendors/feather/feather.css">
    <link rel="stylesheet" href="../../assets/vendors/mdi/css/materialdesignicons.min.css">
    <link rel="stylesheet" href="../../assets/vendors/ti-icons/css/themify-icons.css">
    <link rel="stylesheet" href="../../assets/vendors/font-awesome/css/font-awesome.min.css">
    <link rel="stylesheet" href="../../assets/vendors/typicons/typicons.css">
    <link rel="stylesheet" href="../../assets/vendors/simple-line-icons/css/simple-line-icons.css">
    <link rel="stylesheet" href="../../assets/vendors/css/vendor.bundle.base.css">
    <link rel="stylesheet" href="../../assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.css">
    <!-- endinject -->
    <!-- Plugin css for this page -->
    <!-- End plugin css for this page -->
    <!-- inject:css -->
    <link rel="stylesheet" href="../../assets/css/style.css">
    <!-- endinject -->
    <link rel="shortcut icon" href="../../assets/images/favicon.png" />
    <style>
        body {
            margin-top: 20px;
            background: #f8f8f8
        }
    </style>
    <link rel='stylesheet' href='https://cdn.jsdelivr.net/npm/bootstrap@4.1.1/dist/css/bootstrap.min.css'>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src='https://cdn.jsdelivr.net/npm/bootstrap@4.1.1/dist/js/bootstrap.bundle.min.js'></script>
    <script>
        document.addEventListener("DOMContentLoaded", function () {
            document.querySelectorAll(".edit-btn").forEach(function (button) {
                button.addEventListener("click", function () {
                    // Retrieve author data from the clicked button's attributes
                    const authorName = this.getAttribute("data-authorname");
                    const authorID = this.getAttribute("data-authorID");

                    // Retrieve the input fields for the author name and ID
                    const inputField = document.querySelector(".authorname");
                    const inputField_ID = document.querySelector(".authorID");

                    // Ensure the input fields exist before setting values
                    if (inputField && inputField_ID) {
                        inputField.value = authorName;  // Set the author name in the text input
                        inputField_ID.value = authorID;  // Set the author ID in the hidden input
                    } else {
                        console.error("Input fields with classes 'authorname' or 'authorID' not found.");
                    }
                });
            });
        });
    </script>
</head>
<body class="with-welcome-text">
    <form id="form1" runat="server">
        <div class="container-scroller">
            <!-- partial:partials/_navbar.html -->
            <nav class="navbar default-layout col-lg-12 col-12 p-0 fixed-top d-flex align-items-top flex-row">
                <div class="text-center navbar-brand-wrapper d-flex align-items-center justify-content-start">
                    <div class="me-3">
                        <button class="navbar-toggler navbar-toggler align-self-center" type="button" data-bs-toggle="minimize">
                            <span class="icon-menu"></span>
                        </button>
                    </div>
                    <div>
                        <a class="navbar-brand brand-logo" href="../../AdminDashBoard.aspx">
                            <img src="../../assets/images/logo-v3.png" alt="logo" />
                        </a>
                        <a class="navbar-brand brand-logo-mini" href="../../AdminDashBoard.aspx">
                            <img src="../../assets/images/logo-v3.png" alt="logo" />
                        </a>
                    </div>
                </div>
                <div class="navbar-menu-wrapper d-flex align-items-top">
                    <ul class="navbar-nav">
                        <li class="nav-item fw-semibold d-none d-lg-block ms-0">
                            <h1 class="welcome-text">Giao diện quản lý <span class="text-black fw-bold">Tác giả</span></h1>
                            <h3 class="welcome-sub-text">Thêm, sửa, xoá thông tin của tác giả sách tại thư viện</h3>
                        </li>
                    </ul>
                    <button class="navbar-toggler navbar-toggler-right d-lg-none align-self-center" type="button" data-bs-toggle="offcanvas">
                        <span class="mdi mdi-menu"></span>
                    </button>
                </div>
            </nav>
            <!-- partial -->
            <div class="container-fluid page-body-wrapper">
                <!-- partial:partials/_sidebar.html -->
                <nav class="sidebar sidebar-offcanvas" id="sidebar">
                    <ul class="nav">
                        <li class="nav-item">
                            <a class="nav-link" href="../../AdminDashBoard.aspx">
                                <i class="mdi mdi-grid-large menu-icon"></i>
                                <span class="menu-title">Dashboard</span>
                            </a>
                        </li>
                        <li class="nav-item nav-category">Quản lý thư viện</li>
                        <li class="nav-item">
                            <a class="nav-link" data-bs-toggle="collapse" href="#ui-basic" aria-expanded="false" aria-controls="ui-basic">
                                <i class="menu-icon mdi mdi-floor-plan"></i>
                                <span class="menu-title">Quản lý sách</span>
                                <i class="menu-arrow"></i>
                            </a>
                            <div class="collapse" id="ui-basic">
                                <ul class="nav flex-column sub-menu">
                                    <li class="nav-item"><a class="nav-link" href="authors.aspx">Quản lý tác giả</a></li>
                                    <li class="nav-item"><a class="nav-link" href="categories.aspx">Quản lý thể loại</a></li>
                                    <li class="nav-item"><a class="nav-link" href="books.aspx">Quản lý sách</a></li>
                                </ul>
                            </div>
                        </li>
                        <li class="nav-item">
                            <a class="nav-link" href="#">
                                <i class="menu-icon mdi mdi-account-circle-outline"></i>
                                <span class="menu-title">Quản lý cán bộ</span>
                            </a>
                        </li>
                    </ul>
                </nav>
                <link href="https://maxcdn.bootstrapcdn.com/font-awesome/4.3.0/css/font-awesome.min.css" rel="stylesheet">
                <div class="container">
                    <div class="row flex-lg-nowrap">
                        <div class="col">
                            <div class="e-tabs mb-3 px-3">
                                <ul class="nav nav-tabs">
                                    <li class="nav-item"><a class="nav-link active" href="#">Users</a></li>
                                </ul>
                            </div>


                            <div class="row flex-lg-nowrap">
                                <div class="col mb-3">
                                    <div class="e-panel card">
                                        <div class="card-body">
                                            <div class="card-title">
                                                <h6 class="mr-2"><span>Tác giả</span><small class="px-1">Danh sách tác giả</small></h6>
                                            </div>
                                            <div class="e-table">
                                                <div class="table-responsive table-lg mt-3">
                                                    <table class="table table-bordered">
                                                        <thead>
                                                            <tr>
                                                                <th>STT</th>
                                                                <th class="max-width">Họ và tên</th>
                                                                <th class="sortable">Ngày sinh</th>
                                                                <th>Hành động</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody runat="server" id="tableContent">
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <asp:Panel ID="paginationControls" runat="server" EnableViewState="true"></asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-12 col-lg-3 mb-3">
                                    <div class="card">
                                        <div class="card-body">
                                            <div class="text-center px-xl-3">
                                                <button class="btn btn-success btn-block" type="button" data-toggle="modal" data-target="#user-form-modal">Thêm tác giả</button>
                                            </div>
                                            <br />
                                            <div class="text-center px-xl-3">
                                                <asp:Button
                                                    runat="server"
                                                    Text="Xoá tác giả"
                                                    CssClass="btn btn-danger"
                                                    OnClick="DeleteSelectedAuthors_Click"
                                                    OnClientClick="return confirm('Bạn muốn xoá những tác giả đã chọn?');" />
                                            </div>
                                            <hr class="my-3">
                                            <div>
                                                <div class="form-group">
                                                    <label>Tìm theo tên</label>
                                                    <div>
                                                        <input class="form-control w-100" type="text" name="searchname" placeholder="Name" value="">
                                                    </div>
                                                    <asp:Button
                                                    runat="server"
                                                    Text="Tìm kiếm"
                                                    CssClass="btn btn-primary mt-2"
                                                    OnClick="SearchAuthorByName" />
                                                </div>
                                            </div>
                                            <hr class="my-3">
                                            <div>
                                                <label>Họ và tên tác giả</label>
                                                <input name="authorname" class="authorname" type="text" placeholder="Name" value="">
                                                <input name="authorID" class="authorID" type="hidden" value="">
                                                <br />
                                                <br />
                                                <asp:Button
                                                    runat="server"
                                                    Text="Chỉnh sửa"
                                                    CssClass="btn btn-warning"
                                                    OnClick="UpdateAuthorById" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <!-- User Form Modal -->
                            <div class="modal fade" role="dialog" tabindex="-1" id="user-form-modal">
                                <div class="modal-dialog modal-lg" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title">Thêm tác giả</h5>
                                            <button type="button" class="close" data-dismiss="modal">
                                                <span aria-hidden="true">×</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="py-1">

                                                <div class="row">
                                                    <div class="col">
                                                        <div class="form-group">
                                                            <label>Họ và tên</label>
                                                            <input class="form-control" type="text" name="name" id="authorName" placeholder="Nguyễn Văn A" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Ngày sinh</label>
                                                            <input class="form-control" type="date" name="birthday" id="authorBirthday" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col d-flex justify-content-end">
                                                        <button class="btn btn-primary" type="submit" runat="server" onserverclick="AddAuthor_Click">Thêm tác giả</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- page-body-wrapper ends -->
        </div>
    </form>
    <!-- container-scroller -->
    <!-- plugins:js -->
    <script src="../../assets/vendors/js/vendor.bundle.base.js"></script>
    <script src="../../assets/vendors/bootstrap-datepicker/bootstrap-datepicker.min.js"></script>
    <!-- endinject -->
    <!-- Plugin js for this page -->
    <script src="../../assets/vendors/chart.js/chart.umd.js"></script>
    <script src="../../assets/vendors/progressbar.js/progressbar.min.js"></script>
    <!-- End plugin js for this page -->
    <!-- inject:js -->
    <script src="../../assets/js/off-canvas.js"></script>
    <script src="../../assets/js/template.js"></script>
    <script src="../../assets/js/settings.js"></script>
    <script src="../../assets/js/hoverable-collapse.js"></script>
    <script src="../../assets/js/todolist.js"></script>
    <!-- endinject -->
    <!-- Custom js for this page-->
    <script src="../../assets/js/jquery.cookie.js" type="text/javascript"></script>
    <script src="../../assets/js/dashboard.js"></script>
    <!-- <script src="assets/js/Chart.roundedBarCharts.js"></script> -->
    <!-- End custom js for this page-->
</body>
</html>
