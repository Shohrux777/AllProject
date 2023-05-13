@extends('layouts.dashboardLayout')
@section('title', 'Foydalanuvchilar')

@section('content')
    <header class="page-header">
        <h2>Foydalanuvchilar</h2>

        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Foydalanuvchilar</span></li>
            </ol>

            <a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
        </div>
    </header>
    <div class="row">
        <div class="col-lg-12">

            <section class="panel">
                <header class="panel-heading bg-info">
                    <div class="panel-actions">
                        <a href="#" class="fa fa-caret-down"></a>

                    </div>

                    <h2 class="panel-title">Foydalanuvchilar List</h2>
                </header>
                <div class="panel-body">
                <div class="panel-body">
                    <div class="table-responsive">
                        <table class="table mb-none">
                            <thead>
                                <tr>
                                    <th>№</th>
                                    <th>Имя</th>
                                    <th>Пароль</th>
                                    <th>E-mail</th>
                                    <th>Телефон</th>
                                    <th>Факс</th>
                                    <th>Примечание</th>
                                    <th>Дата</th>
                                    {{-- <th class="text-right mr-3">Actions</th> --}}
                                </tr>
                            </thead>
                            <tbody>
                                @if(count($user)>0)
                                    @foreach ( $user as $userItem )
                                    <tr>
                                        <td>{{$userItem->id}}</td>
                                        <td>{{$userItem->fio}}</td>
                                        <td>{{$userItem->password}}</td>
                                        <td>{{$userItem->gmail}}</td>
                                        <td>{{$userItem->phoneNumber}}</td>
                                        <td>{{$userItem->faks}}</td>
                                        <td>{{$userItem->note}}</td>
                                        <td>{{$userItem->created_at}}</td>
                                        {{-- <td class="actions text-right mr-3">
                                            <form style="display: inline;" action="{{route('news_destroy')}}" method="post">
                                                @csrf
                                                @method('DELETE')
                                                <input type="hidden" name="new_id" value="{{$userItem->id}}">
                                                <input type="submit" style="display: none;" id="{{$userItem->id}}" value="Delete"></input>
                                                <label style="cursor:pointer;" for="{{$userItem->id}}"><i class="fa fa-trash-o"></i></label>
                                            </form>
                                        </td> --}}
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
