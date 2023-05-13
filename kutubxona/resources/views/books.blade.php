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
                    <form class="form-horizontal form-bordered" method="post" action="{{route('bookstore')}}" enctype="multipart/form-data">
                    @csrf
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault1">Kitob nomi</label>
                            <div class="col-md-6">
                                <input type="text" name="name" class="form-control" id="inputDefault1">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputSuccess1">Kategoriya</label>
                            <div class="col-md-6">
                                <select name="category_id" class="form-control input-md mb-md" id="inputSuccess1">
                                    @foreach ( $categories as $category )
                                        <option value="{{$category->id}}">{{$category->name}}</option>
                                    @endforeach
                                </select>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault2">Muallif</label>
                            <div class="col-md-6">
                                <input type="text" name="writer" class="form-control" id="inputDefault2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault3">Til</label>
                            <div class="col-md-6">
                                <input type="text" name="lang" class="form-control" id="inputDefault3">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault4">Nashriyot</label>
                            <div class="col-md-6">
                                <input type="text" name="publisher" class="form-control" id="inputDefault4">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault5">Nashr yili</label>
                            <div class="col-md-6">
                                <input type="text" name="title" class="form-control" id="inputDefault5">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault6">Rasm</label>
                            <div class="col-md-6">
                                <input type="file" name="image" class="form-control" id="inputDefault6">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault7">Kitob pdf</label>
                            <div class="col-md-6">
                                <input type="file" name="pdf" class="form-control" id="inputDefault7">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="textareaDefault1">Kitob haqida</label>
                            <div class="col-md-6">
                                <textarea class="form-control" name="note" rows="4" id="textareaDefault1"></textarea>
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
                                    <th>Kategoriya</th>
                                    <th>Muallif</th>
                                    <th>Nashriyot</th>
                                    <th>Nashr yili</th>
                                    <th>Til</th>
                                    <th class="text-right mr-3">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                @if(count($books)>0)
                                    @foreach ( $books as $book )
                                    <tr>
                                        <td>{{$book->id}}</td>
                                        <td>{{$book->name}}</td>
                                        <td>{{$book->category_name}}</td>
                                        <td>{{$book->writer}}</td>
                                        <td>{{$book->publisher}}</td>
                                        <td>{{$book->title}}</td>
                                        <td>{{$book->lang}}</td>
                                        <td class="actions text-right mr-3">
                                            <a href="{{route('bookEdit', $book->id)}}"><i class="fa fa-pencil"></i></a>
                                            <form style="display: inline;" action="{{route('book_destroy')}}" method="post">
                                                @csrf
                                                @method('DELETE')
                                                <input type="hidden" name="book_id" value="{{$book->id}}">
                                                <input type="submit" style="display: none;" id="{{$book->id}}" value="Delete"></input>
                                                <label style="cursor:pointer;" for="{{$book->id}}"><i class="fa fa-trash-o"></i></label>
                                            </form>
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
