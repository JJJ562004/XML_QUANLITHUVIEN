<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="book-detail.aspx.cs" Inherits="XML_QLTV.book_detail" %>

<!DOCTYPE html>
<html lang="en">
<head>
	<meta charset="UTF-8"/>
	<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
	<meta name="description" content=""/>
	<meta name="author" content=""/>
	<link rel="preconnect" href="https://fonts.googleapis.com"/>
	<link rel="preconnect" href="https://fonts.gstatic.com" crossorigin=""/>
	<link href="https://fonts.googleapis.com/css2?family=Poppins:wght@100;200;300;400;500;600;700;800;900" rel="stylesheet"/>
	<title>DigiMedia</title>
	<!-- Bootstrap core CSS -->
	<link href="vendor/bootstrap/css/bootstrap.min.css" rel="stylesheet"/>
	<!-- Additional CSS Files -->
	<link rel="stylesheet" href="assets/css/fontawesome.css"/>
	<link rel="stylesheet" href="assets/css/templatemo-digimedia-v3.css"/>
	<link rel="stylesheet" href="assets/css/animated.css"/>
	<link rel="stylesheet" href="assets/css/owl.css"/>
	<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
	<header class="header-area header-sticky wow slideInDown" data-wow-duration="0.75s" data-wow-delay="0s">
		<div class="container">
			<div class="row">
				<div class="col-12">
					<nav class="main-nav">
						<!-- ***** Logo Start ***** -->
						<a href="index.html" class="logo">
							<img src="assets/images/logo-v3.png" alt=""/>
						</a>
						<!-- ***** Logo End ***** -->
						<!-- ***** Menu Start ***** -->
						<ul class="nav">
							<li class="scroll-to-section">
								<a href="library.xml" class="active">Trang chủ</a>
							</li>							
						</ul>
						<a class='menu-trigger'>
							<span>Menu</span>
						</a>
						<!-- ***** Menu End ***** -->
					</nav>
				</div>
			</div>
		</div>
	</header>
	<div class="h-100 p-5 bg-body-tertiary border rounded-3">
		<img id="bookImage" runat="server" src="" alt="Book Image" />
    </div>
	<div class="p-5 mb-4 bg-body-tertiary rounded-3">
      <div class="container-fluid py-5">
        <h1 class="display-5 fw-bold" id="bookTitle" runat="server"></h1>
        <p class="col-md-8 fs-4" id="publisherId" runat="server"></p>
		<p class="col-md-8 fs-4" id="categoryId" runat="server"></p>
		<p class="col-md-8 fs-4" id="publishedYear" runat="server"></p>
		<p class="col-md-8 fs-4" id="quantity" runat="server"></p>
		<p class="col-md-8 fs-4" id="description" runat="server"></p>
      </div>
    </div>
		<footer>
			<div class="container">
				<div class="row">
					<div class="col-lg-12">
						<p>
							Copyright © 2022 DigiMedia Co., Ltd. All Rights Reserved.
							<br>
								Design: <a href="https://templatemo.com" target="_parent" title="free css templates">TemplateMo</a>
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
