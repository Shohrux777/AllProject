@extends("private.maket.skelet")

@section("title") Create Company   @endsection
@section("content")
 <div class="row">
                <div class="col-lg-12 col-md-12">
                    <div class="card">
                        <div class="card-header card-header-warning">
                            <h4 class="card-title">Create Company</h4>

                        </div>
                        <div class="card-body table-responsive">
                        <div class="row">
                <div class="col-lg-12 col-md-12">
                    <form action="{{route("company.store")}}" method="post">
                        @csrf
                        <div class="card">
                            
                            <div class="card-body">
                                <div class="form-group">
                                    <div class="form-group">
                                        <input class="form-control" name="name" placeholder="Name"  style="background-color: rgba(190,172,206,0.33)"/>
                                        <br>
                                        <input class="form-control" name="number" placeholder="Number"  style="background-color: rgba(190,172,206,0.33)"/>
                                    </div>
                                </div>
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