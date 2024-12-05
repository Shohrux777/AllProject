@extends("private.maket.skelet")

@section("title") Create Users  @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <h4 class="card-title">Create Users</h4>

                        </div>
                        <div class="card-body table-responsive">
                        <div class="row">
                <div class="col-lg-12 col-md-12">
                    <form action="{{route('usersstore')}}" enctype="multipart/form-data" method="post">
                        @csrf
                        <div class="card">
                            
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        
                                        <input class="form-control" name="fio" placeholder="FullName"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                      
                                        <select class="form-control" name="otdel_name"   style="background-color: rgba(190,172,206,0.33)">
                                        <option>--- Change Otdel ---</option>
                                        @foreach($otdel as $o)
                                        <option value="{{$o->name}}">{{$o->name}}</option>
                                        @endforeach
                                        </select>
                                        <br>
                                        <input class="form-control" name="card_number" placeholder="CardNumber"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <input class="form-control" name="employeeID" placeholder="EmployeeID"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <textarea class="form-control"  style="background-color: rgba(190,172,206,0.33)" name="note" id="" placeholder="Bio" cols="5" rows="5"></textarea>
                                        <br>
                                    </div>
                                </div>
                                <input  type="file" class="form-control" name="image" style="background-color: rgba(190,172,206,0.33)">
                            </div>
                            <div class="card-footer">
                              
                                <button type="submit" class="btn btn-primary">Create</button>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
                        </div>
                    </div>
                </div>
            </div> 
@endsection