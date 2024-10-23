<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@yield('title')</title>

    <!-- Favicons -->
    <link href="{{url('img/favicon.png')}}" rel="icon">
    <link href="{{url('img/apple-touch-icon.png')}}" rel="apple-touch-icon">


    <!-- Google Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,300i,400,400i,600,600i,700,700i|Raleway:300,300i,400,400i,500,500i,600,600i,700,700i|Poppins:300,300i,400,400i,500,500i,600,600i,700,700i" rel="stylesheet">

    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/css/bootstrap.min.css" integrity="sha384-rbsA2VBKQhggwzxH7pPCaAqO46MgnOM80zW1RWuH61DGLwZJEdK2Kadq2F9CUG65" crossorigin="anonymous">
    <!-- Vendor CSS Files -->

    <link rel="stylesheet" href="{{url('css/vendor.min.css')}}" >
    <!-- <link href="https://unpkg.com/aos@2.3.1/dist/aos.css" rel="stylesheet"> -->
    <link href="{{url('vendor/bootstrap/css/bootstrap.min.css')}}" rel="stylesheet">
    <link href="{{url('vendor/bootstrap-icons/bootstrap-icons.css')}}" rel="stylesheet">
    <link href="{{url('vendor/vendar-icons/boxw-icons.css')}}" rel="stylesheet">
    <link href="{{url('vendor/boxicons/css/boxicons.min.css')}}" rel="stylesheet">
    <link href="{{url('vendor/glightbox/css/glightbox.min.css')}}" rel="stylesheet">
    <link href="{{url('vendor/remixicon/remixicon.css')}}" rel="stylesheet">
    <link href="{{url('vendor/swiper/swiper-bundle.min.css')}}" rel="stylesheet">
    <link href='https://unpkg.com/boxicons@2.1.4/css/boxicons.min.css' rel='stylesheet'>
    <!-- Template Main CSS File -->
    <link href="{{url('css/style.css')}}" rel="stylesheet">


</head>
<body>
    <!-- =========Header Top====== -->
    <header class="d-flex align-items-center headdd" style="height:4vh">
        <div class="container-fluid header_topp">
            <div class="row row_setting m-0 p-0">
                <div class="col-lg-6 class_phone p-0">
                    <div class="email_link">
                        <a class="wemail" href="mailto:mirzachol_akm@natlib.uz" target="_blank">mirzachol_akm@natlib.uz</a>
                    </div>
                    <div class="phone_num">
                        <a class="phonee" href="tel:+998997774895">+998(99)-777-48-93</a>
                    </div>
                </div>
                <div class="col-12 col-lg-6 class_icons p-0">
                    <div class="social-links">
                        <a href="https://www.youtube.com/channel/UCtPS7RSE10r2691hIdOkQQg/featured" target="_blank" class="twitter"><i class="bx bxl-youtobe" style="font-size:16px;"></i></a>
                        <a href="https://www.facebook.com/profile.php?id=100087778332042" target="_blank" class="facebook"><i class="bx bxl-facebook" style="font-size:16px;"></i></a>
                        <a href="https://www.instagram.com/mirzachol_akm?r=nametag" target="_blank" class="instagram"><i class="bx bxl-instagram" style="font-size:16px;"></i></a>
                        <a href="https://t.me/MT_AKM" target="_blank" class="google-plus"><i class="bx bxl-telegram" style="font-size:16px;"></i></a>

                    </div>
                </div>
            </div>
        </div>
    </header>

     <!-- ======= Header ======= -->
    <header id="header" class="d-flex align-items-center headerColor" style="height:6vh">
        <div class="container-fluid header_top">
            <div class="row row_nawbar m-0 p-0">
                <a href="/" class="col-6 col-md-4 col-lg-3 col-xl-3 main_logo p-0">
                    <div class="img_box">
                        <img src="assets/images/3.png" alt="png">
                    </div>
                    <div class="logo_text">
                        <h2 class="">Mirzacho'l</h2>
                    </div>
                </a>
                <div class="col-3 col-md-8 col-lg-9 col-xl-9 main_nav p-0">
                    <nav id="navbar" class="navbar">
                        <ul>
                            <li ><a class="nav-link headerText scrollto active" href="/">E'lonlar</a></li>
                            <li ><a class="nav-link headerText scrollto active" href="/">Yangiliklar</a></li>
                            <li ><a class="nav-link headerText scrollto active" href="/">Bosh sahifa</a></li>
                            <li class="dropdown"><a class="headerText" href="#">AKM haqida<i style="font-size: 9.5px;" class="bi bi-chevron-down"></i></a>
                                <ul>
                                <li><a class="headerDownText" href="/hestory">AKM tarxi</a></li>
                                <li><a class="headerDownText" href="/management">Rahbariyat</a></li>
                                <li><a class="headerDownText" href="/systemall">Tuzilma</a></li>
                                <li><a class="headerDownText" href="/workplace">Bo'sh ish o'rinlari</a></li>

                                </ul>
                            </li>

                            <li  class="dropdown"><a class="headerText" href="#">Bo'limlar <i style="font-size: 9.5px;" class="mt-1 bi bi-chevron-down"></i></a>
                                <ul>
                                <li><a class="headerDownText"
                                href="/info1">Abonement va foydalanuvchilarga xizmat ko‘rsatish xizmati</a></li>
                                <li><a class="headerDownText"
                                href="/info2">Axborot-bibliografiya xizmati</a></li>
                                <li><a class="headerDownText"
                                href="/fond1">Fondlarni to‘ldirish, saqlash ishlov berish xizmati</a></li>
                                <li><a href="/komuni"
                                >Axborot kommunikatsiya texnalogiyalari va raqamlashtirish xizmati</a></li>
                                </ul>
                            </li>

                            <li >
                                <a style="font-size: 13.5px;" class="nav-link headerText scrollto" href="#books">Elektron kutubxona</a>
                            </li>

                            <li ><a class="nav-link headerText scrollto" href="/book-buy">Kitob buyurtma berish</a>
                            </li>

                            <li><a class="nav-link headerText scrollto" href="#footer">Biz bilan bog'lanish</a>
                            </li>
                        </ul>
                        <i class="bi bi-list mobile-nav-toggle"></i>
                    </nav>
                </div>
            </div>
        </div>
    </header>

    <!-- End Header -->
    @yield('content')

    <!-- ======= Footer ======= -->
    <footer id="footer">
        <div class="footer-top">
            <div class="container">
                <div class="row row_footer m-0 p-0 w-100">

                    <div class="col-11 col-sm-10 col-md-6 col-lg-4 ">
                        <div class="footer-info">
                            <div class="title_main">
                                <h3>MIRZACHO'L TUMAN AKM</h3>
                            </div>
                            <div class="location_text">
                                <p>Mirzachol tumani,Jizzax,UZB</p>
                            </div>
                            <div class="mail_phone">
                                <p>Qullab quvatlash raqami:</p>
                                <p><a class="phonee" href="tel:+998997774895">+998(99)-777-48-93</a></p>
                                <p><a class="wemail" href="mailto:mirzachol_akm@natlib.uz" target="_blank">Email: mirzachol_akm@natlib.uz</a></p>
                            </div>

                        </div>
                    </div>

                    <div class="col-11 col-sm-10 col-md-6  col-lg-2 footer-links">
                        <h4>Ma'lumot</h4>
                        <ul>
                            <li><i class="bx bx-chevron-right"></i> <a href="#">Rahbariyat</a></li>
                            <li><i class="bx bx-chevron-right"></i> <a href="#">AKM tarxi</a></li>
                            <li><i class="bx bx-chevron-right"></i> <a href="#">Bo'sh ish o'rni</a></li>
                            <li><i class="bx bx-chevron-right"></i> <a href="#">Tuzilma</a></li>
                        </ul>
                    </div>

                    <div class="col-11 col-sm-10 col-md-6 col-lg-3 footer-links">
                        <h4>Biz bilan aloqa</h4>
                        <ul>
                        <li class="li_class"><i class="bx bx-chevron-right"></i> <a href="tel:+998997774895"><i class='bx bxs-phone-call'></i>+998 99 777 48 95</a></li>
                        <li class="li_class"><i class="bx bx-chevron-right"></i> <a href="mailto:mirzachol_akm@natlib.uz" target="_blank"><i class='bx bxs-envelope' ></i>mirzachol_akm@natlib.uz</a></li>
                        <li class="li_class"><i class="bx bx-chevron-right"></i> <a href="#"><i class='bx bx-current-location'></i>Mirzacho'l tumani,Jizzax</a></li>
                        </ul>
                    </div>

                    <div class="col-11 col-sm-10 col-md-6 col-lg-3 footer-links">
                        <h4>Biz ijtimoiy tarmoqlarda</h4>
                            <div class="social-links">
                                <a href="https://www.youtube.com/channel/UCtPS7RSE10r2691hIdOkQQg/featured" target="_blank" class="icon_x twitter"><i class='bx bxl-youtube'></i>
                                <a href="https://www.facebook.com/profile.php?id=100087778332042" target="_blank" class="icon_x facebook"><i class="bx bxl-facebook"></i></a>
                                <a href="https://www.instagram.com/mirzachol_akm?r=nametag" target="_blank" class="icon_x instagram"><i class="bx bxl-instagram"></i></a>
                                <a href="https://t.me/MT_AKM" target="_blank" class="icon_x google-plus"><i class="bx bxl-telegram"></i></a>
                                <a href="#" class="icon_x linkedin"><i class="bx bxl-linkedin"></i></a>
                            </div>

                    </div>

                </div>
            </div>
        </div>
    </footer><!-- End Footer -->



    <a href="#" class="back-to-top d-flex align-items-center justify-content-center"><i class="bi bi-arrow-up-short"></i></a>

    <!-- Vendor JS Files -->
    <!-- <script src="https://unpkg.com/aos@2.3.1/dist/aos.js"></script> -->

    <script src="{{url('vendor/purecounter/purecounter.js')}}"></script>
    <script src="{{url('vendor/bootstrap/js/bootstrap.bundle.min.js')}}"></script>
    <script src="{{url('vendor/glightbox/js/glightbox.min.js')}}"></script>
    <script src="{{url('vendor/isotope-layout/isotope.pkgd.min.js')}}"></script>
    <script src="{{url('vendor/swiper/swiper-bundle.min.js')}}"></script>
    <script src="{{url('vendor/php-email-form/validate.js')}}"></script>

    <!-- Template Main JS File -->
    <script src="{{url('js/main.js')}}"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.min.js" integrity="sha384-cuYeSxntonz0PPNlHhBs68uyIAVpIIOZZ5JqeqvYYIcEL727kskC66kF92t6Xl2V" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.2.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-kenU1KFdBIe4zVF0s0G1M5b4hcpxyD9F7jL+jjXkk+Q2h455rYXK/7HAuoJl+0I4" crossorigin="anonymous"></script>


</body>
</html>
