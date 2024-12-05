@extends("private.maket.skelet")

@section("title") Edit Users   @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <h4 class="card-title">Edit Users</h4>
                            <div class="card-body table-responsive">
                        <div class="row">
                        <div class="col-lg-12 col-md-12">
                            <form action="{{route('uupdate',$data->id)}}" method="put">
                                @csrf
                                <div class="card">
                                    
                                    <div class="card-body">
                                        <div class="form-group">
                                            <div class="form-group">
                                            <input class="form-control" name="fio" placeholder="FullName"  value="{{$data->fio}}"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                      
                                        <select class="form-control" name="otdel_name"   style="background-color: rgba(190,172,206,0.33)">
                                        <option>{{$data->otdel_name}}</option>
                                        @foreach($otdel as $o)
                                        <option value="{{$o->name}}" >{{$o->name}}</option>
                                        @endforeach
                                        </select>
                                        <br>
                                        <input class="form-control" name="card_number"  value="{{$data->card_number}}" placeholder="CardNumber"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <input class="form-control" name="employeeID" value="{{$data->employeeID}}" placeholder="EmployeeID"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <textarea class="form-control"  style="background-color: rgba(190,172,206,0.33)" name="note" id="" placeholder="Bio" cols="5" rows="5">{{$data->note}}</textarea>
                                        <br>
                                                <input type="hidden"   value="{{$data->id}}"/>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="card-footer">
                                    
                                        <button type="submit" class="btn btn-primary">Update</button>
                                    </div>
                                </div>
                                <div class="card-body table-responsive">
                                    
                                </div>
                            </div>
                        </div>
                    </div> 
@endsection