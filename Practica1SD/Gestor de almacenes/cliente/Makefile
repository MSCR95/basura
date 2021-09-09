CFLAGS=-Wall
CC=gcc

all: GestorAlmacenes

GestorAlmacenes: GestorAlmacenes.o GestorAlmacenes_clnt.o GestorAlmacenes_xdr.o
	$(CC) -o $@ GestorAlmacenes.o GestorAlmacenes_clnt.o GestorAlmacenes_xdr.o

GestorAlmacenes.o: GestorAlmacenes.h

clean:
	rm -f *.o GestorAlmacenes
