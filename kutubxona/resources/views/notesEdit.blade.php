@extends('layouts.dashboardLayout')
@section('title', 'Elon tahrirlash')

@section('content')
    <header class="page-header">
        <h2>Elon tahrirlash</h2>

        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Elon tahrirlash</span></li>
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
                    <h2 class="panel-title">Elon tahrirlash</h2>
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
                    <form class="form-horizontal form-bordered"  method="post" action="{{route('notes_update')}}" enctype="multipart/form-data">
                    @csrf
                    @method('PUT')
                    <input type="hidden" name="note_id" value="{{$note->id}}"
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="inputDefault1">Elon mavzusi</label>
                        <div class="col-md-6">
                            <input type="text" value="{{$note->menu}}" name="mavzu" class="form-control" id="inputDefault1">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="inputDefault2">Sana</label>
                        <div class="col-md-6">
                            <input type="text" value="{{$note->datestr}}" name="sana" class="form-control" placeholder="dd-mm-yyyy" id="inputDefault2">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="inputDefault2">Vaqt</label>
                        <div class="col-md-6">
                            <input type="text" value="{{$note->timestr}}" name="vaqt" class="form-control" placeholder="dd-mm-yyyy" id="inputDefault2">
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-3 control-label" for="inputDefault4">Link</label>
                        <div class="col-md-6">
                            <input type="text" name="link" value="{{$note->link}}" class="form-control" id="inputDefault4">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="inputDefault6">Rasm</label>
                        <div class="col-md-6">
                            <input type="file" name="image" value="{{$note->image}}" class="form-control" id="inputDefault6">
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="col-md-3 control-label" for="textareaDefault1">Elon</label>
                        <div class="col-md-6">
                            <textarea class="form-control" name="text" rows="4" id="textareaDefault1">{{$note->text}}</textarea>
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
