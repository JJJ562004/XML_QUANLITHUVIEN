<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="book-detail.aspx.cs" Inherits="XML_QLTV.book_detail" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <link rel="preconnect" href="https://fonts.googleapis.com" />
    <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin="" />
    <link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900" rel="stylesheet" />
    <title>DigiMedia</title>
    <!-- Bootstrap core CSS -->
    <link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <!-- Additional CSS Files -->
    <link rel="stylesheet" href="assets/css/fontawesome.css" />
    <link rel="stylesheet" href="assets/css/templatemo-digimedia-v3.css" />
    <link rel="stylesheet" href="assets/css/animated.css" />
    <link rel="stylesheet" href="assets/css/owl.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <header class="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <nav class="main-nav">
                        <!-- ***** Logo Start ***** -->
                        <a href="library.xml" class="logo">
                            <img src="assets/images/logo-v3.png" alt="" />
                        </a>

                    </nav>
                </div>
            </div>
        </div>
    </header>
    <div class="main-banner wow fadeIn" id="top" data-wow-duration="1s" data-wow-delay="0.5s">

        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="row">
                        <div class="col-lg-6 align-self-center">
                            <div class="left-content show-up header-text wow fadeInLeft animated" data-wow-duration="1s" data-wow-delay="1s" style="visibility: visible; -webkit-animation-duration: 1s; -moz-animation-duration: 1s; animation-duration: 1s; -webkit-animation-delay: 1s; -moz-animation-delay: 1s; animation-delay: 1s;">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <h6 id="bookTitle" runat="server"></h6>
                                        <h2>Nhà sản xuất: <span id="publisherId" runat="server"></span></h2>
                                        <h2>Thể loại: <span id="categoryId" runat="server"></span></h2>
                                        <h2>Năm sản xuất: <span id="publishedYear" runat="server"></span></h2>
                                        <h2>Số lượng: <span id="quantity" runat="server"></span></h2>
                                        <p id="description" runat="server"></p>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="border-first-button scroll-to-section">
                                            <a href="library.xml">Quay lại</a>
                                            <a href="borrow.aspx?id=<%= Request.QueryString["id"] %>">Mượn sách</a>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="right-image wow fadeInRight animated" data-wow-duration="1s" data-wow-delay="0.5s" style="visibility: visible; -webkit-animation-duration: 1s; -moz-animation-duration: 1s; animation-duration: 1s; -webkit-animation-delay: 0.5s; -moz-animation-delay: 0.5s; animation-delay: 0.5s;">
                                <img id="bookImage" runat="server" src="" alt="Book Image" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <footer>
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <p>
                        Copyright © 2022 DigiMedia Co., Ltd. All Rights Reserved.
							<br>Design: <a href="https://templatemo.com" target="_parent" title="free css templates">TemplateMo</a>
                            </br>
                    </p>
                </div>
            </div>
        </div>

    </footer>

    <!-- Scripts -->
    <script src="vendor/jquery/jquery.min.js"></script>
    <script src="vendor/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="assets/js/owl-carousel.js"></script>
    <script src="assets/js/animation.js"></script>
    <script src="assets/js/imagesloaded.js"></script>
    <script src="assets/js/custom.js"></script>
</body>
</html>
