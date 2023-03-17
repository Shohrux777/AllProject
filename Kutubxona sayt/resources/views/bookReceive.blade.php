@extends('layouts.dashboardLayout')
@section('title', 'Kitob buyurtmasi')

@section('content')
<header class="page-header">
        <h2>Kitob buyurmalar</h2>

        <div class="right-wrapper pull-right">
            <ol class="breadcrumbs">
                <li>
                    <a href="index.html">
                        <i class="fa fa-home"></i>
                    </a>
                </li>
                <li><span>Kitob buyurmalar</span></li>
            </ol>

            <a class="sidebar-right-toggle" data-open="sidebar-right"><i class="fa fa-chevron-left"></i></a>
        </div>

    </header>
    <section class="panel">
        <header class="panel-heading bg-info">
            <div class="panel-actions">
                <a href="#" class="fa fa-caret-down"></a>
                <!-- <a href="#" class="fa fa-times"></a> -->
            </div>

            <h2 class="panel-title">Buyurtma List</h2>
        </header>
        <div class="panel-body">
        <div class="panel-body">
            <div class="table-responsive">
                <table class="table mb-none">
                    <thead>
                        <tr>
                            <th>№</th>
                            <th>FIO</th>
                            <th>Tel</th>
                            <th>Kitob nomi</th>
                            <th>Muallif</th>
                            <th>Nashr etilgan yili</th>

                        </tr>
                    </thead>
                    <tbody>
                        @if(count($buyurtmas)>0)
                            @foreach ( $buyurtmas as $buyurtma )
                            <tr>
                                <td>{{$buyurtma->id}}</td>
                                <td>{{$buyurtma->name}}</td>
                                <td>{{$buyurtma->tel}}</td>
                                <td>{{$buyurtma->b_name}}</td>
                                <td>{{$buyurtma->creator}}</td>
                                <td>{{$buyurtma->year}}</td>

                            </tr>
                            @endforeach

                        @else
                            <h5>Buyurtma hali qo'shilmagan</h5>
                        @endif
                    </tbody>
                </table>
            </div>
        </div>
        </div>
    </section>
@endsection
