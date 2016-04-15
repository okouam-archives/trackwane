FROM mono:latest
MAINTAINER Olivier Kouame
ADD ./AccessControl.Service/bin/Debug .
CMD ["mono", "./Trackwane.AccessControl.Service.exe"]
ENV TRACKWANE_ETCD http://etcd:2379
