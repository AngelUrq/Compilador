#Script para buscar terminales y no terminales

noTerminales = []
terminales = []

def buscarTerminalesYNoTerminales(producciones):
    for produccion in producciones:
        noTerminales.append(produccion.split(',')[0])

    for produccion in producciones:
        ladosDerechos = produccion.split(',')[1].split(' ')

        for ladoDerecho in ladosDerechos:
            if not pertenece(noTerminales, ladoDerecho) and ladoDerecho != "€":
                terminales.append(ladoDerecho)

def eliminarRepetidos(lista):
    nuevaLista = []

    for elemento in lista:
        if(not pertenece(nuevaLista,elemento)):
            nuevaLista.append(elemento)

    return nuevaLista

def mostrarTerminalesYNoTerminales():
    print("------------------------------GRAMÁTICA----------------------------")
    for terminal in terminales:
        print("terminales.Add(\"" + terminal + "\");")
    print()
    for noTerminal in noTerminales:
        print("noTerminales.Add(\"" + noTerminal + "\");")
    print("--------------------------------------------------------------------")

def pertenece(lista, elemento):
    for e in lista:
        if elemento == e:
            return True
    return False

def leerArchivo():
    file = open("gramatica.txt","r")
    if file.mode == "r":
        contents = file.read()
        return contents 

def cargarProducciones():
    producciones = leerArchivo().split('\n')
    buscarTerminalesYNoTerminales(producciones)

cargarProducciones()

terminales = eliminarRepetidos(terminales)
noTerminales = eliminarRepetidos(noTerminales)

mostrarTerminalesYNoTerminales()