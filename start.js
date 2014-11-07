var express = require('express');
var server = express();

function vHost(){
    this.vhosts = [];
}
vHost.prototype.handle = function() {
    var self = this;
    return function(req,res,next){

        var domain = req.headers.host.split(':')[0];
        var vhost = null;

        for(i=0;i<self.vhosts.length;i++) {
            if(self.vhosts[i].domain == domain) {
                vhost = self.vhosts[i];
                break;
            }
        }
        if(vhost) {
            //dispatch to vhost
            console.log('dispatch vhost: '+vhost.domain);
            if ('function' == typeof server) return vhost.app(req, res, next);
            vhost.app.emit('request', req, res);
        }else{
            next();
        }

    };
};

vHost.prototype.add = function(domain) {
    var newApp = express();
    newApp.get('/', function(req, res) {
        res.send('klo');
    });
    this.vhosts.push({
        domain: domain,
        path: '/',
        routes: [],
        app: newApp,
    });
};



var vHosts = new vHost('testserver');
//vHosts.add('localhost');
vHosts.add('testserver');


server.use(vHosts.handle() );


server.get('/', function(req,res){
    res.send('isn\'t a vHost');
});
server.listen(8080);







