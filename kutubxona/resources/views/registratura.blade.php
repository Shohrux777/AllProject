@extends('layouts.mainLayout')
@section('title', 'Ro‘yxatdan o‘tish')

@section('content')

    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading container mb-4 border-bottom">
                    <h4 class="panel-title">Ro‘yxatdan o‘tish</h4>
                </header>
                @if(Session::has('alert-success'))
                    <div class="alert alert-success" role="alert">
                        {{Session::get('alert-success')}}
                    </div>
                @endif
                @if(Session::has('alert-info'))
                    <div class="alert alert-info" role="alert">
                        {{Session::get('alert-info')}}
                    </div>
                @endif
                @if(Session::has('server_error'))
                    <div class="alert alert-danger" role="alert">
                        {{Session::get('server_error')}}
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
                <div class="panel-body container">
                    <form class="form-horizontal form-bordered" method="post" action="{{route('registrationStore')}}">
                    @csrf
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="gmail"><span class="text-danger">*</span>E-mail:</label>
                            <div class="col-md-6">
                                <input type="email" name="gmail" class="form-control" id="gmail">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="password"><span class="text-danger">*</span>Пароль:</label>
                            <div class="col-md-6">
                                <input type="password" name="password" class="form-control" id="password">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="returnPassword"><span class="text-danger">*</span>Введите пароль повторно:</label>
                            <div class="col-md-6">
                                <input type="password" name="returnPassword" class="form-control" id="returnPassword">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="fio"><span class="text-danger">*</span>Имя:</label>
                            <div class="col-md-6">
                                <input type="text" name="fio" class="form-control" id="fio">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="phoneNumber"><span class="text-danger">*</span>Телефон:</label>
                            <div class="col-md-6">
                                <input type="text" name="phoneNumber" class="form-control" placeholder="998991234567" id="phoneNumber">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="faks">День рождения : </label>
                            <div class="col-md-6">
                                <input type="date" name="faks" class="form-control"  id="faks">
                            </div>
                        </div>
                        <div class="form-group mt-2">
                            <label class="col-md-3 control-label" for="note">Адрес:</label>
                            <div class="col-md-6">
                                <textarea id="note" name="note" class="form-control" rows="4" ></textarea>
                            </div>
                        </div>
                        <div class=" text-right mt-4">
                            <button type="submit" style="font-size: 12px;" class="mb-xs mt-xs mr-xs btn btn-success">Зарегистрироваться</button>
                        </div>
                    </form>
                </div>
            </section>


        </div>
    </div>
@endsection
