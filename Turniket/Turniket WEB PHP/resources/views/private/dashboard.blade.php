@extends("private.maket.skelet")

@section("title") Admin panel @endsection
@section("content")
    <div class="content">
        <div class="container-fluid">
            {{-- <div class="row" >
                <div class="col-lg-3 col-md-6 col-sm-6">
                    <div class="card card-stats">
                        <div class="card-header card-header-warning card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">schedule</i>
                            </div>
                            <h3 class="card-title">
                            </h3>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <p>Time</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6">
                    <div class="card card-stats">
                        <div class="card-header card-header-success card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">check_circle</i>
                            </div>
                            <h3 class="card-title"> </h3>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <p>Comming</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6">
                    <div class="card card-stats">
                        <div class="card-header card-header-info card-header-icon">
                            <div class="card-icon">
                                <i class="material-icons">subject</i>
                            </div>
                            <h3 class="card-title"> </h3>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <p>All workers</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3 col-md-6 col-sm-6">
                    <div class="card card-stats">
                        <div class="card-header card-header-danger card-header-icon">
                            <div class="card-icon">
                                <i class="fa fa-user"></i>
                            </div>
                            <h3 class="card-title"></h3>
                        </div>
                        <div class="card-footer">
                            <div class="stats">
                                <p>Users</p>
                            </div>
                        </div>
                    </div>
                </div>
            </div> --}}


                            <div class="row">
                                <div class="col-lg-12 col-md-12">
                                    <div class="card">
                                        <div class="card-header " style="background:#1B3444">
                                            <h3 class="card-title text-white">Действия сотрудников</h3>
                                            <p class="m-0 p-0 text-white" style="visibility: hidden">Kelgan xodimlar soni: {{ $xodimlar_soni }}</p>
                                        </div>
                                        <div class="card-body table-responsive">
                                            <table class="table table-hover">
                                                <thead class="text-warning">
                                                <th><b style="color:black">№</b></th>
                                                <th><b style="color:black">ФИО</b></th>
                                                <th><b style="color:black">Идентификатор</b></th>
                                                <th><b style="color:black">Время</b></th>
                                                <th><b style="color:black">Дверь</b></th>



                                                </thead>
                                                <tbody>
                                                @if(count($data) > 0)
                                                @foreach($data as $key => $d)
                                                <tr>
                                                    <td>{{ $key + 1 }}</td>
                                                    <td>{{$d->personName}}</td>
                                                    <td>{{$d->employeeID}}</td>
                                                    <!-- <td>{{$d->authDateTime }}</td> -->
                                                    <td>{{date_format(date_create($d->authDateTime),"Y-m-d H:i:s") }}</td>
                                                    <td>{{$d->deviceName}}</td>

                                                </tr>
                                                @endforeach
                                                @else
                                                    <tr>
                                                        <td class="text-center" colspan="5">empty</td>
                                                    </tr>
                                                @endif

                                                </tbody>
                                            </table>
                                            <div class="pagination justify-content-center">
                                                {{ $data->appends(request()->query())->links() }}
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
        </div>
    </div>

@endsection
