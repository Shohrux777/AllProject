<!doctype html>
<html lang="en">

<head>
    <title>Extres Turnstyle</title>
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta content="width=device-width, initial-scale=1.0" name="viewport" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <!--     Fonts and icons     -->
    <link rel="stylesheet" type="text/css" href="https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Roboto+Slab:400,700|Material+Icons" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/latest/css/font-awesome.min.css">
    @yield("css_files")
    <!-- Material Kit CSS -->
    <link href="{{asset("back/css/material-dashboard.css?v=2.1.2")}}" rel="stylesheet" />
</head>

<body>
<div class="wrapper ">
    <div class="sidebar" data-color="purple" data-background-color="white">
        <div class="logo"><span class="simple-text logo-normal">
        Extres Turnstyle
            </span></div>
        <div class="sidebar-wrapper">
            <ul class="nav">
                <li class="nav-item {{ Request::is('dashboard') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route("a.dashboard")}}">
                        <i class="material-icons">dashboard</i>
                        <p>Мониторинг</p>
                    </a>
                </li>

                <li class="nav-item {{ Request::is('appeals') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route('filter2')}}">

                        <i class="material-icons">manage_search</i>
                        <p>Отчет</p>
                    </a>
                </li>

                <li class="nav-item {{ Request::is('appeals') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route("company.index")}}">
                        <i class="material-icons">domain_disabled</i>
                        <p>Компания</p>
                    </a>
                </li>

                <li class="nav-item {{ Request::is('appeals') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route("otdel.index")}}">
                        <i class="material-icons">store</i>
                        <p>Отдел</p>
                    </a>
                </li>

                <li class="nav-item {{ Request::is('appeals') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route("users.index")}}">
                        <i class="material-icons">manage_accounts</i>
                        <p>Сотрудники</p>
                    </a>
                </li>
                <li class="nav-item {{ Request::is('appeals') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route('filter')}}">
                        <i class="material-icons">person_off</i>
                        <p>Не существует</p>
                    </a>
                </li>
                <li class="nav-item {{ Request::is('options') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route('filter3')}}">
                        <i class="material-icons">person_add</i>
                        <p>Существует</p>
                    </a>
                </li>

                <li class="nav-item {{ Request::is('options') ? 'active' : '' }}">
                    <a class="nav-link" href="{{route('filter4')}}">
                        <i class="material-icons">analytics</i>
                        <p>Информация</p>
                    </a>
                </li>

                <li class="nav-item">
                    <a class="nav-link" href="{{route("a.logout")}}">
                        <i class="material-icons">logout</i>
                        <p>Выйти</p>
                    </a>
                </li>

            </ul>
        </div>
    </div>
    <div class="main-panel">
        <!-- Navbar -->
        <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top ">
            <div class="container-fluid">
                <button class="navbar-toggler" type="button" data-toggle="collapse" aria-controls="navigation-index" aria-expanded="false" aria-label="Toggle navigation">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="navbar-toggler-icon icon-bar"></span>
                    <span class="navbar-toggler-icon icon-bar"></span>
                    <span class="navbar-toggler-icon icon-bar"></span>
                </button>
                <div class="collapse navbar-collapse justify-content-end">
                    <ul class="navbar-nav">
                        <li class="nav-item">
                            <a class="nav-link" href="{{route("a.logout")}}">
                                <i class="material-icons">logout</i> Log out
                            </a>
                        </li>
                        <!-- your navbar here -->
                    </ul>
                </div>
            </div>
        </nav>
        <!-- End Navbar -->

        <div class="content">
            <div class="container-fluid">
                <!-- your content here -->
                @php
                    use Illuminate\Support\Facades\Session;if(Session::has('message')):
                        if(Session::get('m_status') == 1)
                            $color = "success";
                        else
                            $color = "danger";
                @endphp
                <div class="alert alert-{{$color}}"> {!!  Session::get("message")!!}
                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"> <span aria-hidden="true">&times;</span> </button>
                </div>

                @endif

                @if ($errors->any())
                    <div class="alert alert-danger">
                        <ul>
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif

                @yield("content")
            </div>
        </div>
        <footer class="footer">
            <div class="container-fluid">
                <div class="copyright float-right">
                    &copy;
                    <script>
                        document.write(new Date().getFullYear())
                    </script>
                    <a href="" target="_blank"></a>
                </div>
                <!-- your footer here -->
            </div>
        </footer>


    </div>
</div>


<!--   Core JS Files   -->
<script src="{{asset("back/js/core/jquery.min.js")}}"></script>
<script src="{{asset("back/js/core/popper.min.js")}}"></script>
<script src="{{asset("back/js/core/bootstrap-material-design.min.js")}}"></script>
<script src="{{asset("back/js/plugins/perfect-scrollbar.jquery.min.js")}}"></script>
<!-- Plugin for the momentJs  -->
<script src="{{asset("back/js/plugins/moment.min.js")}}"></script>
<!--  Plugin for Sweet Alert -->
<script src="{{asset("back/js/plugins/sweetalert2.js")}}"></script>
<!-- Forms Validations Plugin -->
<script src="{{asset("back/js/plugins/jquery.validate.min.js")}}"></script>
<!-- Plugin for the Wizard, full documentation here: https://github.com/VinceG/twitter-bootstrap-wizard -->
<script src="{{asset("back/js/plugins/jquery.bootstrap-wizard.js")}}"></script>
<!--	Plugin for Select, full documentation here: http://silviomoreto.github.io/bootstrap-select -->
<script src="{{asset("back/js/plugins/bootstrap-selectpicker.js")}}"></script>
<!--  Plugin for the DateTimePicker, full documentation here: https://eonasdan.github.io/bootstrap-datetimepicker/ -->
<script src="{{asset("back/js/plugins/bootstrap-datetimepicker.min.js")}}"></script>
<!--  DataTables.net Plugin, full documentation here: https://datatables.net/  -->
<script src="{{asset("back/js/plugins/jquery.dataTables.min.js")}}"></script>
<!--	Plugin for Tags, full documentation here: https://github.com/bootstrap-tagsinput/bootstrap-tagsinputs  -->
<script src="{{asset("back/js/plugins/bootstrap-tagsinput.js")}}"></script>
<!-- Plugin for Fileupload, full documentation here: http://www.jasny.net/bootstrap/javascript/#fileinput -->
<script src="{{asset("back/js/plugins/jasny-bootstrap.min.js")}}"></script>
<!--  Full Calendar Plugin, full documentation here: https://github.com/fullcalendar/fullcalendar    -->
<script src="{{asset("back/js/plugins/fullcalendar.min.js")}}"></script>
<!-- Vector Map plugin, full documentation here: http://jvectormap.com/documentation/ -->
<script src="{{asset("back/js/plugins/jquery-jvectormap.js")}}"></script>
<!--  Plugin for the Sliders, full documentation here: http://refreshless.com/nouislider/ -->
<script src="{{asset("back/js/plugins/nouislider.min.js")}}"></script>
<!-- Include a polyfill for ES6 Promises (optional) for IE11, UC Browser and Android browser support SweetAlert -->
<script src="https://cdnjs.cloudflare.com/ajax/libs/core-js/2.4.1/core.js"></script>
<!-- Library for adding dinamically elements -->
<script src="{{asset("back/js/plugins/arrive.min.js")}}"></script>
<!--  Google Maps Plugin    -->
<script src="https://maps.googleapis.com/maps/api/js?key=YOUR_KEY_HERE"></script>
<!-- Chartist JS -->
<script src="{{asset("back/js/plugins/chartist.min.js")}}"></script>
<!--  Notifications Plugin    -->
<script src="{{asset("back/js/plugins/bootstrap-notify.js")}}"></script>
<!-- Control Center for Material Dashboard: parallax effects, scripts for the example pages etc -->
<script src="{{asset("back/js/material-dashboard.js?v=2.1.2")}}" type="text/javascript"></script>
@yield("js_files")
<script>
    $(document).ready(function() {
        $().ready(function() {
            $sidebar = $('.sidebar');

            $sidebar_img_container = $sidebar.find('.sidebar-background');

            $full_page = $('.full-page');

            $sidebar_responsive = $('body > .navbar-collapse');

            window_width = $(window).width();

            fixed_plugin_open = $('.sidebar .sidebar-wrapper .nav li.active a p').html();

            if (window_width > 767 && fixed_plugin_open == 'Dashboard') {
                if ($('.fixed-plugin .dropdown').hasClass('show-dropdown')) {
                    $('.fixed-plugin .dropdown').addClass('open');
                }

            }

            $('.fixed-plugin a').click(function(event) {
                // Alex if we click on switch, stop propagation of the event, so the dropdown will not be hide, otherwise we set the  section active
                if ($(this).hasClass('switch-trigger')) {
                    if (event.stopPropagation) {
                        event.stopPropagation();
                    } else if (window.event) {
                        window.event.cancelBubble = true;
                    }
                }
            });

            $('.fixed-plugin .active-color span').click(function() {
                $full_page_background = $('.full-page-background');

                $(this).siblings().removeClass('active');
                $(this).addClass('active');

                var new_color = $(this).data('color');

                if ($sidebar.length != 0) {
                    $sidebar.attr('data-color', new_color);
                }

                if ($full_page.length != 0) {
                    $full_page.attr('filter-color', new_color);
                }

                if ($sidebar_responsive.length != 0) {
                    $sidebar_responsive.attr('data-color', new_color);
                }
            });

            $('.fixed-plugin .background-color .badge').click(function() {
                $(this).siblings().removeClass('active');
                $(this).addClass('active');

                var new_color = $(this).data('background-color');

                if ($sidebar.length != 0) {
                    $sidebar.attr('data-background-color', new_color);
                }
            });

            $('.fixed-plugin .img-holder').click(function() {
                $full_page_background = $('.full-page-background');

                $(this).parent('li').siblings().removeClass('active');
                $(this).parent('li').addClass('active');


                var new_image = $(this).find("img").attr('src');

                if ($sidebar_img_container.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                    $sidebar_img_container.fadeOut('fast', function() {
                        $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                        $sidebar_img_container.fadeIn('fast');
                    });
                }

                if ($full_page_background.length != 0 && $('.switch-sidebar-image input:checked').length != 0) {
                    var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                    $full_page_background.fadeOut('fast', function() {
                        $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
                        $full_page_background.fadeIn('fast');
                    });
                }

                if ($('.switch-sidebar-image input:checked').length == 0) {
                    var new_image = $('.fixed-plugin li.active .img-holder').find("img").attr('src');
                    var new_image_full_page = $('.fixed-plugin li.active .img-holder').find('img').data('src');

                    $sidebar_img_container.css('background-image', 'url("' + new_image + '")');
                    $full_page_background.css('background-image', 'url("' + new_image_full_page + '")');
                }

                if ($sidebar_responsive.length != 0) {
                    $sidebar_responsive.css('background-image', 'url("' + new_image + '")');
                }
            });

            $('.switch-sidebar-image input').change(function() {
                $full_page_background = $('.full-page-background');

                $input = $(this);

                if ($input.is(':checked')) {
                    if ($sidebar_img_container.length != 0) {
                        $sidebar_img_container.fadeIn('fast');
                        $sidebar.attr('data-image', '#');
                    }

                    if ($full_page_background.length != 0) {
                        $full_page_background.fadeIn('fast');
                        $full_page.attr('data-image', '#');
                    }

                    background_image = true;
                } else {
                    if ($sidebar_img_container.length != 0) {
                        $sidebar.removeAttr('data-image');
                        $sidebar_img_container.fadeOut('fast');
                    }

                    if ($full_page_background.length != 0) {
                        $full_page.removeAttr('data-image', '#');
                        $full_page_background.fadeOut('fast');
                    }

                    background_image = false;
                }
            });

            $('.switch-sidebar-mini input').change(function() {
                $body = $('body');

                $input = $(this);

                if (md.misc.sidebar_mini_active == true) {
                    $('body').removeClass('sidebar-mini');
                    md.misc.sidebar_mini_active = false;

                    $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar();

                } else {

                    $('.sidebar .sidebar-wrapper, .main-panel').perfectScrollbar('destroy');

                    setTimeout(function() {
                        $('body').addClass('sidebar-mini');

                        md.misc.sidebar_mini_active = true;
                    }, 300);
                }

                // we simulate the window Resize so the charts will get updated in realtime.
                var simulateWindowResize = setInterval(function() {
                    window.dispatchEvent(new Event('resize'));
                }, 180);

                // we stop the simulation of Window Resize after the animations are completed
                setTimeout(function() {
                    clearInterval(simulateWindowResize);
                }, 1000);

            });
        });
    });
</script>
</body>

</html>
