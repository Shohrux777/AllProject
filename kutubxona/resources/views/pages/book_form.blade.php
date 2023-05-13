@extends('layouts.mainLayout')
@section('title', 'buy books')

@section('content')
  <div class="all_x" id="form">
    <div class="container">
      <div class="row row_form m-0 p-0 w-100">
        <div class="col-10 col-sm-10 col-md-8 col-lg-6 col-xl-6">
          <div  class="form_box">
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
              <div class="form_title">
                <h4>Kitob buyurtma bering</h4>
              </div>
              <form class="form_action" method="post" action="{{route('mainstore')}}">
                @csrf
                    <div class="form_input">
                        <input type="text" name="name" placeholder="F.I.O">
                    </div>
                    <div class="form_input">
                        <input id="phone" name="tel" type="text" placeholder="+998(__)-___-__-__" data-mask="+000(00)-000-00-00">
                    </div>
                    <div class="form_input">
                        <input type="text" name="b_name" placeholder="Kitob nomi">
                    </div>
                    <div class="form_input">
                        <input type="text" name="creator" placeholder="Muallif(lar)">
                    </div>
                    <div class="form_input">
                        <input type="text" name="year" placeholder="Nashr etilgan yili" maxlength="4" data-mask="0000">
                    </div>
                    <div class="form_btn">
                        <button type="submit">Buyurtma berish</button>
                    </div>
                </form>
          </div>
        </div>
      </div>
    </div>
  </div>
@endsection