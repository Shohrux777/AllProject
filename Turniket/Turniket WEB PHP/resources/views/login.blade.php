<!doctype html>
<html lang="en">

<head>
    <title>Авторизоваться</title>
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
        <div class="content">
            <div class="container-fluid">
                <div class="content">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-4 col-sm-12"></div>
                <div class="col-lg-4 col-sm-12 p-3">

                    @php
                        use Illuminate\Support\Facades\Session;if(Session::has('message')):
                            if(Session::get('m_status') == 1)
                                $color = "success";
                            else
                                $color = "danger";
                    @endphp
                    <div class="alert alert-{{$color}}"> {!!  Session::get("message")!!}
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"></button>
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

                    <div class="card card-stats p-4">
                        <h3 class="card-title">Авторизоваться</h3>

                        <form action="{{route('a.login')}}" method="post" >
                            @csrf

                            <div class="form-group">
                                <input type="text" placeholder="Логин" required class="form-control" name="username">
                            </div>

                            <div class="form-group">
                                <input type="password" placeholder="Пароль" required class="form-control" name="password">
                            </div>

                            <div class="form-group">
                                <button type="submit" class="btn btn-primary">Вход</button>
                            </div>

                        </form>

                    </div>
                </div>
                <div class="col-lg-4 col-sm-12"></div>
            </div>
        </div>
    </div>

    </div>
    </div>
    </div>

    </body>

    </html>

