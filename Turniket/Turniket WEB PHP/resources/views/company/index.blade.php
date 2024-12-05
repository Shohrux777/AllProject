@extends("private.maket.skelet")

@section("title") Crud Company   @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div style="background:#1B3444" class="card-header  d-flex justify-content-between align-items-center">
                            <h3 class="card-title p-0 m-0 text-white">Компания</h3>
                            <a  href="{{route('company.create')}}" class="btn btn-primary text-white m-0" style="visibility:hidden"><i class="fa fa-plus-square" aria-hidden="true"></i> Create</a>
                        </div>
                        <div class="card-body table-responsive">
                            <table class="table table-hover">
                                <thead class="text-warning">
                                <th>Идентификатор</th>
                                <th>Названный</th>
                                <!-- <th style="visibility:hidden">Action</th> -->

                                </thead>
                                <tbody>
                                @if(count($data) > 0)
                                @foreach($data as $d)
                                <tr>
                                    <td>{{$d->id}}</td>
                                    <td>{{$d->name}}</td>

                                    <!-- <td style = "visibility:hidden">
                                    <a href="{{ route('company.edit',$d->id) }}" class="btn btn-primary text-white"><i class="fa fa-edit"></i></a>
                                    <a href="{{ route('companydelete',$d->id) }}" class="btn btn-primary text-white"><i class="fa fa-trash" aria-hidden="true"></i></a>

                                </td> -->
                                </tr>
                                @endforeach
                                @else
                                    <tr>
                                        <td class="text-center" colspan="5">Результатов не найдено</td>
                                    </tr>
                                @endif

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
@endsection
