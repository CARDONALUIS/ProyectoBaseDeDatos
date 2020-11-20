grammar GramaticaSQL;

/*
 * Parser Rules
 */

consulta: SELECT atributo ESP from ESP tabla condicion;

atributo:ESP aste
		| ESP NOM atributo ?;
		 

SELECT: 'SELECT';

NOM: [A-Z]+;

ESP: ' '; 

from: 'FROM';

aste: '*';

tabla: NOM;

condicion: (ESP where)?;

where: 'WHERE';



//: 'Int' | 'Double' | 'Float';


 /*
 consulta: SELECT ESP atributos EOF;// ESP FROM ESP NOMTAB EOF;

 //atributos: ASTER | NOMATR;
 atributos:atributos atributo
		  | atributo;

 atributo: ASTER 
		 | NOMATR ;*/

 

/*
 * Lexer Rules
 */

 /*
SELECT: 'SELECT';

NOMATR: [A-Z]+;

ESP: ' '; 

ASTER: '*';

FROM: 'FROM';

NOMTAB: [A-Z]+;

FINL:['\r\n']+;*/

/*compileUnit
	:	EOF
	;*/


/*
WS
	:	' ' -> channel(HIDDEN)
	;
*/