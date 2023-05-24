@extends('layouts.dashboardLayout')
@section('title', 'Elonlar')

@section('content')
    <header class="page-header">
        <h2>Elonlar</h2>

        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Elonlar</span></li>
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
                    <h2 class="panel-title">Elonlar</h2>
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
                    <form class="form-horizontal form-bordered" method="post" action="{{route('notestore')}}" enctype="multipart/form-data">
                    @csrf
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault1">Elon mavzusi</label>
                            <div class="col-md-6">
                                <input type="text" name="mavzu" class="form-control" id="inputDefault1">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault2">Sana</label>
                            <div class="col-md-6">
                                <input type="text" name="sana" class="form-control" placeholder="25 - May" id="inputDefault2">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault2">Vaqt</label>
                            <div class="col-md-6">
                                <input type="text" name="vaqt" class="form-control" placeholder="12:00 - 15:30" id="inputDefault2">
                            </div>
                        </div>

                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault4">Link</label>
                            <div class="col-md-6">
                                <input type="text" name="link" class="form-control" id="inputDefault4">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="inputDefault6">Rasm</label>
                            <div class="col-md-6">
                                <input type="file" name="image" class="form-control" id="inputDefault6">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-3 control-label" for="textareaDefault1">Elon</label>
                            <div class="col-md-6">
                                <textarea class="form-control" name="text" rows="4" id="textareaDefault1"></textarea>
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

                    <h2 class="panel-title">Elonlar List</h2>
                </header>
                <div class="panel-body">
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table mb-none">
                            <thead>
                                <tr>
                                    <th>№</th>
                                    <th>Elon mavzusi</th>
                                    <th>Sana</th>
                                    <th>Vaqt</th>
                                    <th>Link</th>
                                    <th>Elon</th>
                                    <th class="text-right mr-3">Actions</th>
                                </tr>
                            </thead>
                            <tbody>
                                @if(count($notifications)>0)
                                    @foreach ( $notifications as $note )
                                    <tr>
                                        <td>{{$note->id}}</td>
                                        <td>{{$note->menu}}</td>
                                        <td>{{$note->datestr}}</td>
                                        <td>{{$note->timestr}}</td>
                                        <td>{{$note->link}}</td>
                                        <td>{{$note->text}}</td>
                                        <td class="actions text-right mr-3">
                                            <a href="{{route('notesEdit', $note->id)}}"><i class="fa fa-pencil"></i></a>
                                            <form style="display: inline;" action="{{route('notes_destroy')}}" method="post">
                                                @csrf
                                                @method('DELETE')
                                                <input type="hidden" name="note_id" value="{{$note->id}}">
                                                <input type="submit" style="display: none;" id="{{$note->id}}" value="Delete"></input>
                                                <label style="cursor:pointer;" for="{{$note->id}}"><i class="fa fa-trash-o"></i></label>
                                            </form>
                                        </td>
                                    </tr>
                                    @endforeach

                                @else
                                    <h5>Elonlar hali qo'shilmagan</h5>
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
