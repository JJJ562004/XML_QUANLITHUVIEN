<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="html" indent="yes"/>

	<xsl:template match="/Library">
		<html>
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
											<a href="#top" class="active">Trang chủ</a>
										</li>
										<li class="scroll-to-section">
											<a href="#about">Về chúng tôi</a>
										</li>
										<li class="scroll-to-section">
											<a href="#services">Sách hot</a>
										</li>
										<li class="scroll-to-section">
											<a href="#portfolio">Sách</a>
										</li>
										<li class="scroll-to-section">
											<a href="#blog">Tác giả</a>
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

				<div class="main-banner wow fadeIn" id="top" data-wow-duration="1s" data-wow-delay="0.5s">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="row">
									<div class="col-lg-6 align-self-center">
										<div class="left-content show-up header-text wow fadeInLeft" data-wow-duration="1s" data-wow-delay="1s">
											<div class="row">
												<div class="col-lg-12">
													<h6>DigiMedia</h6>
													<h2>
														Hệ Thống Mượn Sách Thư Viện
													</h2>
													<p>
														Hệ thống này giúp bạn đơn giản hóa việc mượn sách từ thư viện. Hãy thoải mái sử dụng nền tảng để tìm kiếm, đặt trước và quản lý các lượt mượn sách của bạn một cách tiện lợi. Lưu ý, việc phân phối lại mã nguồn hoặc tài nguyên của hệ thống này mà không được phép là không hợp lệ. Cảm ơn bạn đã sử dụng hệ thống quản lý thư viện của chúng tôi.
													</p>
												</div>
												<div class="col-lg-12">
													<div class="border-first-button scroll-to-section">
														<a href="#about">Về chúng tôi</a>
													</div>
												</div>
											</div>
										</div>
									</div>
									<div class="col-lg-6">
										<div class="right-image wow fadeInRight" data-wow-duration="1s" data-wow-delay="0.5s">
											<img src="assets/images/slider-dec-v3.png" alt=""/>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>				

				<div id="services" class="services section">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="section-heading  wow fadeInDown" data-wow-duration="1s" data-wow-delay="0.5s">
									<h6>Thể loại</h6>
									<h4>
										Top sách <em>HOT</em>
									</h4>
									<div class="line-dec"></div>
								</div>
							</div>
							<div class="col-lg-12">
								<div class="naccs">
									<div class="grid">
										<div class="row">
											<div class="col-lg-12">
												<div class="menu">
													<div class="first-thumb active">
														<div class="thumb">
															<span class="icon">
																<img src="assets/images/service-icon-02.png" alt="Category Icon"/>
															</span>
															<xsl:value-of select="Categories/Category[1]/CategoryName"/>
														</div>
													</div>
													<xsl:for-each select="Categories/Category[position() > 1]">
														<div class="thumb">
															<span class="icon">
																<img src="assets/images/service-icon-02.png" alt="Category Icon"/>
															</span>
															<xsl:value-of select="CategoryName"/>
														</div>
													</xsl:for-each>
												</div>
											</div>
											<xsl:variable name="firstcategoryID" select="Categories/Category[1]/CategoryID" />
											<xsl:variable name="firstcategoryName" select="Categories/Category[1]/CategoryName" />
											<xsl:variable name="firstMostBorrowedBook">
												<xsl:for-each select="/Library/Books/Book[CategoryID = $firstcategoryID]">
													<xsl:sort select="count(/Library/BorrowingRecords/BorrowingRecord[BookID = BookID])" order="descending"/>
													<xsl:if test="position() = 1">
														<xsl:value-of select="Title"/>
													</xsl:if>
												</xsl:for-each>
											</xsl:variable>
											<div class="col-lg-12">
												<ul class="nacc">
													<li class="active">
														<div>
															<div class="thumb">
																<div class="row">
																	<div class="col-lg-6 align-self-center">
																		<div class="left-text">
																			<h4>
																				<xsl:value-of select="$firstMostBorrowedBook"/>
																			</h4>
																			<div class="ticks-list">
																				<span>
																					<xsl:value-of select="$firstcategoryName" />
																				</span>
																			</div>
																			<p>
																				Số lần được mượn nhiều nhất: <xsl:value-of select="count(/Library/BorrowingRecords/BorrowingRecord[BookID = BookID])"/>
																			</p>
																		</div>
																	</div>
																	<div class="col-lg-6 align-self-center">
																		<div class="right-image">
																			<xsl:for-each select="/Library/Books/Book[CategoryID = $firstcategoryID]">
																				<xsl:sort select="count(/Library/BorrowingRecords/BorrowingRecord[BookID = current()/BookID])" order="descending"/>
																				<xsl:if test="position() = 1">
																					<img src="{ImageURL}" alt="Book Image" />
																				</xsl:if>
																			</xsl:for-each>
																		</div>
																	</div>
																</div>
															</div>
														</div>
														<div>
															<div class="container-fluid wow fadeIn" data-wow-duration="1s" data-wow-delay="0.7s">
																<div class="row">
																	<div class="col-lg-12">
																		<div class="loop owl-carousel">
																			<xsl:for-each select="/Library/Books/Book[CategoryID = $firstcategoryID]">
																				<div class="item">
																					<a href="book-detail.aspx?id={BookID}">
																						<div class="portfolio-item">
																							<div class="thumb">
																								<img src="{ImageURL}" alt="{Title}" />
																							</div>
																							<div class="down-content">
																								<h4>
																									<xsl:value-of select="Title" />
																								</h4>
																								<span>
																									Năm xuất bản: <xsl:value-of select="PublishedYear" />
																								</span>
																							</div>
																						</div>
																					</a>
																				</div>
																			</xsl:for-each>
																		</div>
																	</div>
																</div>
															</div>
														</div>
													</li>
													<xsl:for-each select="Categories/Category[position() > 1]">
														<xsl:variable name="categoryID" select="CategoryID" />
														<xsl:variable name="categoryName" select="CategoryName" />
														<xsl:variable name="booksInCategory" select="/Library/Books/Book[CategoryID = $categoryID]" />
														<xsl:choose>
															<xsl:when test="count($booksInCategory) > 0">
																<xsl:variable name="mostBorrowedBook">
																	<xsl:for-each select="/Library/Books/Book[CategoryID = $categoryID]">
																		<xsl:sort select="count(/Library/BorrowingRecords/BorrowingRecord[BookID = current()/BookID])" order="descending" />
																		<xsl:if test="position() = 1">
																			<xsl:value-of select="Title"/>
																		</xsl:if>
																	</xsl:for-each>
																</xsl:variable>
																<li>
																	<div>
																		<div class="thumb">
																			<div class="row">
																				<div class="col-lg-6 align-self-center">
																					<div class="left-text">
																						<h4>
																							<xsl:value-of select="$mostBorrowedBook" />
																						</h4>
																						<div class="ticks-list">
																							<span>
																								<xsl:value-of select="$categoryName" />
																							</span>
																						</div>
																						<p>
																							Số lần được mượn nhiều nhất:
																							<xsl:value-of select="count(/Library/BorrowingRecords/BorrowingRecord[BookID = BookID])" />
																						</p>
																					</div>
																				</div>
																				<div class="col-lg-6 align-self-center">
																					<div class="right-image">
																						<img src="assets/images/portfolio-03.jpg" alt="Book Image" />
																					</div>
																				</div>
																			</div>
																		</div>
																	</div>
																</li>
															</xsl:when>
															<xsl:otherwise>
																<li>Chưa có sách đã mượn.</li>
															</xsl:otherwise>
														</xsl:choose>
													</xsl:for-each>

												</ul>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>


				<div id="portfolio" class="our-portfolio section">
					<div class="container">
						<div class="row">
							<div class="col-lg-5">
								<div class="section-heading wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.3s">
									<h6>SÁCH CỦA THƯ VIỆN</h6>
									<h4>
										Xem tổng quát <em>SÁCH</em>
									</h4>
									<div class="line-dec"></div>
								</div>
							</div>
						</div>
					</div>
					<div class="container-fluid wow fadeIn" data-wow-duration="1s" data-wow-delay="0.7s">
						<div class="row">
							<div class="col-lg-12">
								<div class="loop owl-carousel">
									<xsl:for-each select="Books/Book">
										<div class="item">
											<a href="book-detail.aspx?id={BookID}">
												<div class="portfolio-item">
													<div class="thumb">
														<img src="{ImageURL}" alt="{Title}" />
													</div>
													<div class="down-content">
														<h4>
															<xsl:value-of select="Title" />
														</h4>
														<span>
															Năm xuất bản: <xsl:value-of select="PublishedYear" />
														</span>
													</div>
												</div>
											</a>
										</div>
									</xsl:for-each>
								</div>
							</div>
						</div>
					</div>
				</div>

				<!-- Start Tác giả -->
				<div id="blog" class="services section">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="section-heading  wow fadeInDown" data-wow-duration="1s" data-wow-delay="0.5s">
									<h6>Tác giả</h6>
									<h4>
										Giới thiệu <em>TÁC GIẢ</em>
									</h4>
									<div class="line-dec"></div>
								</div>
							</div>
							<div class="col-lg-12">
								<div class="naccs">
									<div class="grid">
										<div class="row">
											<div class="col-lg-12">
												<div class="menu">
													<div class="first-thumb">
														<div class="thumb">
															<span class="icon">
																<img src="assets/images/author-post.jpg" alt="Category Icon"/>
															</span>
															<xsl:value-of select="Authors/Author[1]/AuthorName"/>
														</div>
													</div>
													<xsl:for-each select="Authors/Author[position() > 1]">
														<div class="thumb">
															<span class="icon">
																<img src="assets/images/author-post.jpg" alt="Category Icon"/>
															</span>
															<xsl:value-of select="AuthorName"/>
														</div>
													</xsl:for-each>
												</div>
											</div>

											<div class="col-lg-12">
												<ul class="nacc">
													<xsl:for-each select="Authors/Author">
														<li>
															<div>
																<div class="thumb">
																	<div class="row">
																		<div class="col-lg-6 align-self-center">
																			<div class="left-text">
																				<h4>
																					<xsl:value-of select="AuthorName" />
																				</h4>
																				<div class="ticks-list">
																					<span>
																						Xin chào, tôi là <xsl:value-of select="AuthorName" />. Là người đam mê viết truyện, tôi luôn cố gắng sáng tạo nên những câu chuyện cuốn hút, giàu trí tưởng tượng và sâu sắc. Mỗi tác phẩm của tôi là một nỗ lực để kết nối cảm xúc và đưa độc giả đến những miền không gian mới mẻ.
																					</span>
																				</div>
																			</div>
																		</div>
																		<div class="col-lg-6 align-self-center">
																			<div class="right-image">
																				<img src="assets/images/author-post.jpg" alt="Book Image" />
																			</div>
																		</div>
																	</div>
																</div>
															</div>
														</li>
													</xsl:for-each>
												</ul>
											</div>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<!-- End Tác giả -->
				
				<div id="about" class="about section">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="row">
									<div class="col-lg-6">
										<div class="about-left-image  wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.5s">
											<img src="assets/images/about-dec-v3.png" alt=""/>
										</div>
									</div>
									<div class="col-lg-6 align-self-center  wow fadeInRight" data-wow-duration="1s" data-wow-delay="0.5s">
										<div class="about-right-content">
											<div class="section-heading">
												<h6>Về chúng tôi</h6>
												<h4>
													<em>DigiMedia</em> là gì
												</h4>
												<div class="line-dec"></div>
											</div>
											<p>
												Chúng tôi hy vọng hệ thống quản lý mượn sách này sẽ hữu ích cho công việc của bạn.
											</p>
										</div>
									</div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="about0" class="services section">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="section-heading  wow fadeInDown" data-wow-duration="1s" data-wow-delay="0.5s">
									<h6>Thành viên nhóm</h6>
									<h4>
										Dưới đây là <em>thành viên nhóm</em>
									</h4>
									<div class="line-dec"></div>
								</div>
							</div>
						</div>
					</div>
				</div>
				<div id="about1" class="about section">
					<div class="container">
						<div class="row">
							<div class="col-lg-12">
								<div class="row">
									<div class="col-lg-6">
										<div class="about-left-image  wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.5s">
											<img src="assets/images/tuan.jpg" height="500" alt=""/>
										</div>
									</div>
									<div class="col-lg-6 align-self-center  wow fadeInRight" data-wow-duration="1s" data-wow-delay="0.5s">
										<div class="about-right-content">
											<div class="section-heading">
												<h6>Nguyễn Ngọc Tuấn</h6>
												<div class="line-dec"></div>
											</div>
											<div class="about-right-content">
												<div class="section-heading">
													<h6></h6>
													<h4>
														Đóng góp vào <em>DigiMedia</em>
													</h4>
												</div>
											</div>
											<div class="row">
												<div class="col-lg-4 col-sm-4">
													<div class="skill-item first-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
														<div class="progress" data-percentage="100">
															<span class="progress-left">
																<span class="progress-bar"></span>
															</span>
															<span class="progress-right">
																<span class="progress-bar"></span>
															</span>
															<div class="progress-value">
																<div>
																	100%
																	<h5>Trang người dùng</h5>
																</div>
															</div>
														</div>
													</div>
												</div>
												<div class="col-lg-4 col-sm-4">
													<div class="skill-item second-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
														<div class="progress" data-percentage="40">
															<span class="progress-left">
																<span class="progress-bar"></span>
															</span>
															<span class="progress-right">
																<span class="progress-bar"></span>
															</span>
															<div class="progress-value">
																<div>
																	40%
																	<h5>Phân quyền</h5>
																</div>
															</div>
														</div>
													</div>
												</div>
												<div class="col-lg-4 col-sm-4">
													<div class="skill-item third-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
														<div class="progress" data-percentage="15">
															<span class="progress-left">
																<span class="progress-bar"></span>
															</span>
															<span class="progress-right">
																<span class="progress-bar"></span>
															</span>
															<div class="progress-value">
																<div>
																	20%
																	<h5>Trang Admins</h5>
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
					</div>
					<div id="about0" class="services section">
						<div class="container">
							<div class="row">
								<div class="col-lg-12">
									<div class="section-heading  wow fadeInDown" data-wow-duration="1s" data-wow-delay="0.5s">
										<div class="line-dec"></div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div id="about2" class="about section">
						<div class="container">
							<div class="row">
								<div class="col-lg-12">
									<div class="row">
										<div class="col-lg-6">
											<div class="about-left-image  wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.5s">
												<img src="assets/images/hieu.jpg" height="500" alt=""/>
											</div>
										</div>
										<div class="col-lg-6 align-self-center  wow fadeInRight" data-wow-duration="1s" data-wow-delay="0.5s">
											<div class="about-right-content">
												<div class="section-heading">
													<h6>Phạm Thanh Hiếu</h6>
													<div class="line-dec"></div>
												</div>
												<div class="about-right-content">
													<div class="section-heading">
														<h6></h6>
														<h4>
															Đóng góp vào <em>DigiMedia</em>
														</h4>
													</div>
												</div>
												<div class="row">
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item first-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="30">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		30%
																		<h5>Trang người dùng</h5>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item second-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="10">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		10%
																		<h5>Phân quyền</h5>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item third-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="100">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		100%
																		<h5>Trang Admins</h5>
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
						</div>
					</div>
					<div id="about0" class="services section">
						<div class="container">
							<div class="row">
								<div class="col-lg-12">
									<div class="section-heading  wow fadeInDown" data-wow-duration="1s" data-wow-delay="0.5s">
										<div class="line-dec"></div>
									</div>
								</div>
							</div>
						</div>
					</div>
					<div id="about3" class="about section">
						<div class="container">
							<div class="row">
								<div class="col-lg-12">
									<div class="row">
										<div class="col-lg-6">
											<div class="about-left-image  wow fadeInLeft" data-wow-duration="1s" data-wow-delay="0.5s">
												<img src="assets/images/binh.jpg" height="500" alt=""/>
											</div>
										</div>
										<div class="col-lg-6 align-self-center  wow fadeInRight" data-wow-duration="1s" data-wow-delay="0.5s">
											<div class="about-right-content">
												<div class="section-heading">
													<h6>Trần Văn Bình</h6>
													<div class="line-dec"></div>
												</div>
												<div class="about-right-content">
													<div class="section-heading">
														<h6></h6>
														<h4>
															Đóng góp vào <em>DigiMedia</em>
														</h4>
													</div>
												</div>
												<div class="row">
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item first-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="40">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		40%
																		<h5>Trang người dùng</h5>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item second-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="100">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		100%
																		<h5>Phân quyền</h5>
																	</div>
																</div>
															</div>
														</div>
													</div>
													<div class="col-lg-4 col-sm-4">
														<div class="skill-item third-skill-item wow fadeIn" data-wow-duration="1s" data-wow-delay="0s">
															<div class="progress" data-percentage="10">
																<span class="progress-left">
																	<span class="progress-bar"></span>
																</span>
																<span class="progress-right">
																	<span class="progress-bar"></span>
																</span>
																<div class="progress-value">
																	<div>
																		10%
																		<h5>Trang Admins</h5>
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
						</div>
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
	</xsl:template>
</xsl:stylesheet>
