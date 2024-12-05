@extends("private.maket.skelet")

@section("title") Crud Workers   @endsection
@section("content")
<style>
    .button-3 {
  appearance: none;
  background-color: #2ea44f;
  border: 1px solid rgba(27, 31, 35, .15);
  border-radius: 6px;
  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;
  box-sizing: border-box;
  color: #fff;
  cursor: pointer;
  display: inline-block;
  font-family: -apple-system,system-ui,"Segoe UI",Helvetica,Arial,sans-serif,"Apple Color Emoji","Segoe UI Emoji";
  font-size: 14px;
  font-weight: 600;
  line-height: 20px;
  padding: 6px 16px;
  position: relative;
  text-align: center;
  text-decoration: none;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  vertical-align: middle;
  white-space: nowrap;
}

.button-4 {
  appearance: none;
  background-color:crimson;
  border: 1px solid rgba(27, 31, 35, .15);
  border-radius: 6px;
  box-shadow: rgba(27, 31, 35, .1) 0 1px 0;
  box-sizing: border-box;
  color: #fff;
  cursor: pointer;
  display: inline-block;
  font-family: -apple-system,system-ui,"Segoe UI",Helvetica,Arial,sans-serif,"Apple Color Emoji","Segoe UI Emoji";
  font-size: 14px;
  font-weight: 600;
  line-height: 20px;
  padding: 6px 16px;
  position: relative;
  text-align: center;
  text-decoration: none;
  user-select: none;
  -webkit-user-select: none;
  touch-action: manipulation;
  vertical-align: middle;
  white-space: nowrap;
}

</style>
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div style="background:#1B3444" class="card-header  d-flex justify-content-between align-items-center">
                            <h3 class="card-title p-0 m-0 text-white">Сотрудники</h3>
                            <a  href="{{route('users.create')}}" class="btn btn-primary text-white m-0" style="visibility:hidden"><i class="fa fa-plus-square" aria-hidden="true"></i> Create</a>
                        </div>
                        <div class="card-body table-responsive">
                            <table class="table table-hover">
                                <thead class="text-warning">
                                <th>Идентификатор</th>
                                <th>ФИО</th>                               
                                <th>Отдел</th>
                                <th>Состояние</th>
                                

                                </thead>
                                <tbody>
                                @if(count($data) > 0)
                                @foreach($data as $d)
                                <tr>
                                    <td>{{$d->employeeID}}</td>
                                    <td>{{$d->fio}}</td>
                                    <td>{{$d->otdel_name}}</td>
                                    <td>@if($d->status == 1)

                                     <button class="button-3 p-0 m-0 py-1 px-3" role="button">Активный</button>
                                        @else
                                        <button class="button-4 p-0 m-0 py-1 px-3" >Деактивировано</button>
                                        @endif
                                        </td>
                                    {{-- <td><img style="width: 100px;height:100px" src="data:image/jpeg;base64, {{ $d->image}}" name="image"></td> --}}
                        
                                </tr>
                                @endforeach
                                @else
                                    <tr>
                                        <td class="text-center" colspan="5">Empty</td>
                                    </tr>
                                @endif

                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
@endsection
