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
                <li><span>Kategoriya</span></li>
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
                    <form class="form-horizontal form-bordered" method="post" action="{{route('store')}}">
                    @csrf
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault">Kategoriya</label>
                            <div class="col-md-6">
                                <input type="text" name="name" class="form-control" id="inputDefault">
                            </div>
                        </div>
                        <div class=" text-right">
                            <button type="submit" style="font-size: 12px;" class="mb-xs mt-xs mr-xs btn btn-success">Saqlash</button>
                        </div>
                    </form>
                </div>
            </section>

            <section class="panel">
                <header class="panel-heading bg-info">
                    <div class="panel-actions">
                        <a href="#" class="fa fa-caret-down"></a>
                        <!-- <a href="#" class="fa fa-times"></a> -->
                    </div>

                    <h2 class="panel-title">Kategoriya List</h2>
                </header>
                <div class="panel-body">
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table mb-none">
                            <thead>
                                <tr>
                                    <th>№</th>
                                    <th>Name</th>
                                    <th class="text-right mr-3">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                @if(count($categories)>0)
                                    @foreach ( $categories as $category )
                                    <tr>
                                        <td>{{$category->id}}</td>
                                        <td>{{$category->name}}</td>
                                        <td class="actions text-right mr-3 ">
                                            <a href="{{route('bookCategoryEdit', $category->id)}}"><i class="fa fa-pencil"></i></a>
                                            <form style="display: inline;" action="{{route('category_destroy')}}" method="post">
                                                @csrf
                                                @method('DELETE')
                                                <input type="hidden" name="category_id" value="{{$category->id}}">
                                                <input type="submit" style="display: none;" id="{{$category->id}}" value="Delete"></input>
                                                <label style="cursor:pointer;" for="{{$category->id}}"><i class="fa fa-trash-o"></i></label>
                                            </form>
                                            {{-- <a href="{{route('category_destroy', $category->id)}}" class="delete-row"></a> --}}
                                        </td>
                                    </tr>
                                    @endforeach

                                @else
                                    <h5>Kategoriya hali qo'shilmagan</h5>
                                @endif
                            </tbody>
                        </table>
                    </div>
                </div>
                </div>
            </section>

        </div>
    </div>
    
@endsection
