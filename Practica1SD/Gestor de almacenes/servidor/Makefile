CFLAGS=-Wall
CC=gcc

all: GestorAlmacenesd

GestorAlmacenesd: GestorAlmacenesd.o GestorAlmacenes_svc.o GestorAlmacenes_xdr.o
	$(CC) -g -o $@ GestorAlmacenesd.o GestorAlmacenes_svc.o GestorAlmacenes_xdr.o

GestorAlmacenesd.o: GestorAlmacenes.h

clean:
	rm -f *.o GestorAlmacenesd
