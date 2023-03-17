@extends('layouts.dashboardLayout')
@section('title', 'Kategoriya')

@section('content')
    <header class="page-header">
        <h2>Kategoriya</h2>
        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Kategoriyani tahrirlash</span></li>
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
                    <h2 class="panel-title">Kategoriya</h2>
                </header>
                @if(Session::has('alert-success'))
                    <div class="alert alert-success" role="alert">
                        {{Session::get('alert-success')}}
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
                    <form class="form-horizontal form-bordered" method="post" action="{{route('category_update')}}">
                    @csrf
                    @method('PUT')
                        <input type="hidden" name="category_id" value="{{$category->id}}"
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault">Kategoriya</label>
                            <div class="col-md-6">
                                <input type="text" name="name" value="{{$category->name}}" class="form-control" id="inputDefault">
                            </div>
                        </div>
                        <div class=" text-right">
                            <button type="submit" style="font-size: 12px;" class="mb-xs mt-xs mr-xs btn btn-success">Saqlash</button>
                        </div>
                    </form>
                </div>
            </section>
        </div>
    </div>
@endsection
