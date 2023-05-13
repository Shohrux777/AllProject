@extends('layouts.dashboardLayout')
@section('title', 'Kitoblar')

@section('content')
    <header class="page-header">
        <h2>Kitoblar</h2>

        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Kitoblar</span></li>
            </ol>

            <a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
        </div>
    </header>
    <div class="row">
        <div class="col-lg-12">
            <section class="panel">
                <header class="panel-heading">
                    <div class="panel-actions">
                        <a href="#" class="fa fa-caret-down"></a>
                        <!-- <a href="#" class="fa fa-times"></a> -->
                    </div>
                    <h2 class="panel-title">Kitoblar</h2>
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
                <div class="panel-body">
                    <form class="form-horizontal form-bordered"  method="post" action="{{route('book_update')}}" enctype="multipart/form-data">
                    @csrf
                    @method('PUT')
                    <input type="hidden" name="book_id" value="{{$book->id}}"
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault1">Kitob nomi</label>
                            <div class="col-md-6">
                                <input type="text" name="name" value="{{$book->name}}" class="form-control" id="inputDefault1">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputSuccess1">Kategoriya</label>
                            <div class="col-md-6">
                                <select name="category_id" class="form-control input-md mb-md" id="inputSuccess1">
                                    <option value="{{$book->category_id}}" selected>{{$book->category_name}}</option>
                                    @foreach ( $categories as $category )
                                        <option value="{{$category->id}}">{{$category->name}}</option>
                                    @endforeach
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault2">Muallif</label>
                            <div class="col-md-6">
                                <input type="text" name="writer" value="{{$book->writer}}" class="form-control" id="inputDefault2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault3">Til</label>
                            <div class="col-md-6">
                                <input type="text" name="lang" value="{{$book->lang}}" class="form-control" id="inputDefault3">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault4">Nashriyot</label>
                            <div class="col-md-6">
                                <input type="text" name="publisher" value="{{$book->publisher}}" class="form-control" id="inputDefault4">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault5">Nashr yili</label>
                            <div class="col-md-6">
                                <input type="text" name="title" value="{{$book->title}}" class="form-control" id="inputDefault5">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault6">Rasm</label>
                            <div class="col-md-6">
                                <input type="file" name="image" value="{{$book->image}}" class="form-control" id="inputDefault6">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault7">Kitob pdf</label>
                            <div class="col-md-6">
                                <input type="file" name="pdf" value="{{$book->pdf}}" class="form-control" id="inputDefault7">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="textareaDefault1">Kitob haqida</label>
                            <div class="col-md-6">
                                <textarea class="form-control" name="note" rows="4" id="textareaDefault1">{{$book->note}}</textarea>
                            </div>
                        </div>
                        <div class=" text-right">
                            <button type="submit" style="font-size: 12px;" class="mb-xs mt-xs mr-xs btn btn-success">Tahrirlash</button>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
@endsection
